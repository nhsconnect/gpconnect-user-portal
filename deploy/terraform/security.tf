# K8s worker security group
data "aws_security_group" "worker_sg" {
  filter {
    name   = "tag:Name"
    values = ["live-lk8s-nonprod-live-leks-cluster"]
  }
}

# Security group for database
resource "aws_security_group" "database_servers" {
  vpc_id      = data.aws_vpc.default.id
  name_prefix = module.vars.env.prefix
  description = "The NDSP database"
  tags = {
    Name = "${module.vars.env.prefix}-database-servers"
  }
}

# TODO delete this when we have Security Groups for Pods
resource "aws_security_group_rule" "k8s_to_postgres" {
  security_group_id        = aws_security_group.database_servers.id
  type                     = "ingress"
  description              = "${module.vars.env.prefix}: postgresdb +---[postgres]--- k8s"
  from_port                = aws_rds_cluster.default.port
  to_port                  = aws_rds_cluster.default.port
  protocol                 = "tcp"
  source_security_group_id = data.aws_security_group.worker_sg.id
}
