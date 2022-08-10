
provider "aws" {
  region = "eu-west-2"

  default_tags {
    tags = {
      Service = "gpc-ndsp"
    }
  }
}

provider "kubernetes" {

}
