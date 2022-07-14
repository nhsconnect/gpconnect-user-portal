
data "aws_subnets" "default" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.default.id]
  }
  filter {
    name   = "mapPublicIpOnLaunch"
    values = ["false"]
  }
}

resource "aws_db_subnet_group" "default" {
  name       = "${local.prefix}-db-subnet-group"
  subnet_ids = data.aws_subnets.default.ids
}

resource "random_password" "default" {
  length  = 20
  special = true
}

resource "aws_rds_cluster" "default" {
  cluster_identifier   = "${local.prefix}-database"
  db_subnet_group_name = aws_db_subnet_group.default.name

  engine            = "aurora-postgresql"
  engine_mode       = "provisioned"
  engine_version    = "14.3"
  storage_encrypted = true

  database_name   = "postgres"
  master_username = "postgres"
  master_password = random_password.database_password.result

  vpc_security_group_ids = [
    aws_security_group.database_servers.id
  ]

  serverlessv2_scaling_configuration {
    max_capacity = 1.0
    min_capacity = 0.5
  }

  tags = {
    Name = "${local.prefix}-database"
  }
}

resource "aws_rds_cluster_instance" "default" {
  cluster_identifier = aws_rds_cluster.default.id

  identifier     = "${local.prefix}-database-single-instance"
  engine         = aws_rds_cluster.default.engine
  engine_version = aws_rds_cluster.default.engine_version
  instance_class = "db.serverless"

  tags = {
    Name = "${local.prefix}-database-instance"
  }
}
