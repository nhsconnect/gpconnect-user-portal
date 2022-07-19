
## First Deployment

We need to overcome the chicken/egg problem of performing a database migration
which includes the users and their assignment to IAM roles, when we don't have
any users assigned to IAM roles to start with.

For the first deployment into an environment

- Run once manually and target the database password secret

e.g.

```sh
aws-vault exec [profile] -- terraform plan -target aws_secretsmanager_secret.database_password -out /tmp/create-password.plan
aws-vault exec [profile] -- terraform apply /tmp/create-password.plan
```

- Populate the password field
- Then run again with the full config (via CI/CD should be OK)
