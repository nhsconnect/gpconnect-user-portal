pipeline {
  agent any

  stages {

    stage('Build') {
      steps {
        sh 'make build-containers'
      }
    }

    stage('Test') {
      steps {
        sh 'make test'
        sh 'docker compose -f docker-compose.build.yml down'
      }
    }

    stage('Acceptance') {
      steps {
        sh 'make serve'
        sh 'make acceptance-test'
        sh 'docker compose down'
      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploying...'
      }
    }

  }
}
