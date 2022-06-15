APP_SERVICES := auth-server mock-api end-user-portal admin-portal database

migrate:
	docker compose up database data-migrator --exit-code-from data-migrator

serve:
	docker compose up --build -d $(APP_SERVICES)

acceptance-test:
	docker compose up --build $@ --exit-code-from $@

