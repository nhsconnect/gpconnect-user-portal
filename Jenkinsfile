def awsRegion = 'eu-west-2'
def ecrAccountId = '461183108257'

def dockerRepo = 'gpc-ndsp'
def version = 'latest'

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

    stage('Push to ECR'){
      steps {
        def ecrImageName = "${ecrAccountId}.dkr.ecr.${awsRegion}.amazonaws.com/${dockerImage}"
        // This example uses a multi-stage build
        // sh """docker build -t ${dockerImage}-build:${version} -f ${dockerDir}/pipeline-build.dockerfile ."""
        // sh """docker build --build-arg VERSION=${version} -t ${ecrImageName}:${version} -f ${dockerDir}/pipeline-runtime.dockerfile ."""

        // Create repo if it doesn't exist
        sh """aws ecr describe-repositories --repository-names ${dockerRepo} || aws ecr create-repository --repository-name ${dockerRepo}"""
        // Push images to ECR

        def images = [
          // 'database-migrator', // TODO : need to package this with schema for deployment in cluster
          'api',
          'end-user-portal',
        ]

        // Authenticate against ECR
        sh """aws ecr get-login-password --region ${awsRegion} | docker login --username AWS --password-stdin ${ecrAccountId}.dkr.ecr.eu-west-2.amazonaws.com"""

        for (image in images) {
          fullImageName = "${dockerRepo}/${image}"
          fullImageId = "${ecrAccountId}.dkr.ecr.${awsRegion}.amazonaws.com/${fullImageName}"
          sh """aws ecr describe-repositories --repository-names ${fullImageName} || aws ecr create-repository --repository-name ${fullImageName}"""
          sh """docker tag ndsp/${image} ${fullImageId}"""
          sh """docker push ${fullImageId}:${version}"""
        }
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
