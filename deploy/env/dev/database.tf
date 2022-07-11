

resource "aws_db_subnet_group" "default" {
  name = "${local.prefix}-db-subnet-group"
  subnet_ids = [
    data.aws_subnet.private.id
  ]
}

resource "aws_rds_cluster" "default" {
  cluster_identifier   = "${local.prefix}-database"
  db_subnet_group_name = aws_db_subnet_group.default.name

  engine          = "aurora-postgresql"
  engine_mode     = "provisioned"
  engine_version  = "14.3"
  database_name   = "postgres"
  master_username = "postgres"
  master_password = "Lo1waUYrTXJsXj"

  vpc_security_group_ids = [
    aws_security_group.database_servers.id
  ]

  serverlessv2_scaling_configuration {
    max_capacity = 0.5
    min_capacity = 0.5
  }
}

resource "aws_rds_cluster_instance" "default" {
  cluster_identifier = aws_rds_cluster.default.id
  engine             = aws_rds_cluster.default.engine
  engine_version     = aws_rds_cluster.default.engine_version
  instance_class     = "db.serverless"
}
