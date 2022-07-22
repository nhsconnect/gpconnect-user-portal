
APP_SERVICES :=	end-user-portal
TEST_SERVICES := $(APP_SERVICES) auth-server mock-api database

migrate:
	docker compose up database data-migrator --exit-code-from data-migrator

serve:
	docker compose up --build -d $(APP_SERVICES)

acceptance-test:
	docker compose build $@
	docker compose up $@ --exit-code-from $@

build-containers:
	docker compose -f docker-compose.build.yml up --build

test:
	docker compose -f docker-compose.build.yml run api dotnet test
	docker compose -f docker-compose.build.yml run end-user-portal dotnet test
