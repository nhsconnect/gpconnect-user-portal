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
      }
    }

    stage('Acceptance') {
      steps {
        sh 'make serve'
        sh 'make acceptance-test'
      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploying...'
      }
    }

  }
}
