docker build -t 658583182001.dkr.ecr.us-east-1.amazonaws.com/alledrogo/backend:latest .
docker rm alledrogo-backend
docker run -p 8080:8080 `
    --name alledrogo-backend `
    -e COGNITO_AUTHORITY="https://cognito-idp.us-east-1.amazonaws.com/us-east-1_bHfx5Ulow" `
    -e COGNITO_USER_POOL_ID="us-east-1_bHfx5Ulow" `
    -e DATABASE_CONNECTION_STRING="Host=alledrogo-postgresql.czcs88g82ce0.us-east-1.rds.amazonaws.com:5432;Database=alledrogo;Username=postgres;Password=Terraform123!" `
    -e FRONTEND_IP="frontend-lb-81148153.us-east-1.elb.amazonaws.com" `
    -e SQS_QUEUE_URL="https://sqs.us-east-1.amazonaws.com/658583182001/message_queue" `
    658583182001.dkr.ecr.us-east-1.amazonaws.com/alledrogo/backend:latest