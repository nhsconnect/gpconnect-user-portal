
## Terraform

Initialize config (once)

```sh
terraform init
```

Plan config changes

This needs AWS creds in context e.g.

```sh
aws-vault exec texas-nonprod -- terraform plan
```

Apply config changes

```sh
aws-vault exec texas-nonprod -- terraform apply
```
