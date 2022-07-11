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
        // sh 'make acceptance-test'
      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploying...'
      }
    }
  }

  post {
    always {
      sh 'docker compose down'
    }
  }
}
