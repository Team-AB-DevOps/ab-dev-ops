terraform {
  required_providers {
    digitalocean = {
      source  = "digitalocean/digitalocean"
      version = "2.44.0"
    }
  }
}

provider "digitalocean" {
  # Configuration options
  token = var.do_token
}

data "digitalocean_ssh_key" "devops" {
  name = "devops"
}
