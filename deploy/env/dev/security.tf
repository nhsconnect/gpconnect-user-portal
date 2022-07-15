# K8s worker security group
# TODO : migrate to Security Groups for Pods
data "aws_security_group" "worker_sg" {
  filter {
    name   = "tag:Name"
    values = ["live-lk8s-nonprod-live-leks-cluster"]
  }
}

# Security group for API (and Flyway)
resource "aws_security_group" "database_clients" {
  vpc_id      = data.aws_vpc.default.id
  name_prefix = local.prefix
  description = "Clients of the NDSP database"
  tags = {
    Name = "${local.prefix}-database-clients"
  }
}

# Security group for database
resource "aws_security_group" "database_servers" {
  vpc_id      = data.aws_vpc.default.id
  name_prefix = local.prefix
  description = "The NDSP database"
  tags = {
    Name = "${local.prefix}-database-servers"
  }
}

# Security group rules for database clients
resource "aws_security_group_rule" "postgres_out_of_api" {
  security_group_id        = aws_security_group.database_clients.id
  description              = "${local.prefix}: API ---[postgres]---+ postrgres DB"
  type                     = "egress"
  from_port                = aws_rds_cluster.default.port
  to_port                  = aws_rds_cluster.default.port
  protocol                 = "tcp"
  source_security_group_id = aws_security_group.database_servers.id
}

# Security group rules for database
resource "aws_security_group_rule" "api_to_postgres" {
  security_group_id        = aws_security_group.database_servers.id
  type                     = "ingress"
  description              = "${local.prefix}: postgresdb +---[postgres]--- API"
  from_port                = aws_rds_cluster.default.port
  to_port                  = aws_rds_cluster.default.port
  protocol                 = "tcp"
  source_security_group_id = aws_security_group.database_clients.id
}

# TODO delete this when we have Security Groups for Pods
resource "aws_security_group_rule" "k8s_to_postgres" {
  security_group_id        = aws_security_group.database_servers.id
  type                     = "ingress"
  description              = "${local.prefix}: postgresdb +---[postgres]--- k8s"
  from_port                = aws_rds_cluster.default.port
  to_port                  = aws_rds_cluster.default.port
  protocol                 = "tcp"
  source_security_group_id = data.aws_security_group.worker_sg.id
}
