variable "cluster_name" {
  default = "k8s-ipassport"
}

variable "aws_region" {
  default = "us-east-1"
}

variable "k8s_version" {
  default = "1.28"
}

variable "nodes_instances_sizes" {
  default = [
    "t3.large"
  ]
}

variable "auto_scale_options" {
  default = {
    min     = 2
    max     = 10
    desired = 2
  }
}

variable "aws_pg_db_name" {
  type        = string
  default = "ipassport"
}

variable "aws_pg_allocated_storage" {
  type        = number
  default = 10
}

variable "aws_pg_engine" {
    type = string
    default = "postgres"
}

variable "aws_pg_engine_version" {
    type = string
    default = "14.4"
}

variable "aws_pg_instance_class" {
    type = string
    default = "db.t3.micro"
}

variable "aws_pg_username"{
    type = string
    default = "passport"
}

variable "aws_pg_password" {
  type = string
  default= "LRbMsucwwaILnM94oJTVvfSJFhrpa9iSYBggcJon8"
}

variable "aws_bucket_name" {
  type        = string
  default = "ipassport"
}

variable "aws_bucket_env" {
  type        = string
  default = "hmg"
}

