# Create a new Droplet
resource "digitalocean_droplet" "monitoring" {
  image   = "ubuntu-24-10-x64"
  name    = "monitoring"
  region  = "fra1"
  size    = "s-2vcpu-2gb"
  backups = true
  ssh_keys = [
    data.digitalocean_ssh_key.devops.id
  ]
  backup_policy {
    plan    = "weekly"
    weekday = "TUE"
    hour    = 8
  }
}

# Create a reserved IP
resource "digitalocean_reserved_ip" "monitoring-ip" {
  droplet_id = digitalocean_droplet.monitoring.id
  region     = digitalocean_droplet.monitoring.region
}

# Assign firewall rules
resource "digitalocean_firewall" "monitoring" {
  name = "monitoring-firewall"

  depends_on = [digitalocean_droplet.monitoring]

  droplet_ids = [digitalocean_droplet.monitoring.id]

  inbound_rule {
    protocol         = "tcp"
    port_range       = "22"
    source_addresses = ["192.168.1.0/24", "2002:1:2::/48"]
  }

  inbound_rule {
    protocol         = "tcp"
    port_range       = "80"
    source_addresses = ["0.0.0.0/0", "::/0"]
  }

  inbound_rule {
    protocol         = "tcp"
    port_range       = "443"
    source_addresses = ["0.0.0.0/0", "::/0"]
  }

  outbound_rule {
    protocol              = "icmp"
    destination_addresses = ["0.0.0.0/0", "::/0"]
  }
  outbound_rule {
    protocol              = "tcp"
    port_range            = "1-65535"
    destination_addresses = ["0.0.0.0/0", "::/0"]
  }
  outbound_rule {
    protocol              = "udp"
    port_range            = "1-65535"
    destination_addresses = ["0.0.0.0/0", "::/0"]
  }

}

