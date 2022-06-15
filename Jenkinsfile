pipeline {
  agent any

  stages {

    stage('Build') {
      steps {
        sh 'make serve'
        sh 'make acceptance-test'
      }
    }

    stage('Test') {
      steps {
        echo 'Testing...'
      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploying...'
      }
    }

  }
}
