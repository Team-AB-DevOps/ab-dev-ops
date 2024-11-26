variable "do_token" {
  description = "The access token for DigitalOcean API."
  type        = string
  sensitive   = true
}

variable "pvt_key" {
  description = "The private SSH key to access the droplets."
  type        = string
  sensitive   = true
}
