def awsRegion = 'eu-west-2'
def ecrAccountId = '461183108257'

def dockerRepo = 'gpc-ndsp'
def version = 'latest'

node {

  try {

    stage('Build') {
      sh 'make build-containers'
    }

    stage('Test') {
      sh 'make test'
      sh 'docker compose -f docker-compose.build.yml down'
    }

    stage('Acceptance') {
      sh 'make serve'
      // sh 'make acceptance-test'
    }

    stage('Push to ECR'){

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

    stage('Deploy') {
      echo 'Deploying...'
    }

  }

  finally {
    sh 'docker compose down'
  }
}
