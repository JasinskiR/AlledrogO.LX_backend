docker run -p 8080:8080 `
    -e COGNITO_AUTHORITY="https://cognito-idp.us-east-1.amazonaws.com/us-east-1_bHfx5Ulow" `
    -e COGNITO_USER_POOL_ID="us-east-1_bHfx5Ulow" `
    -e DATABASE_CONNECTION_STRING="Host=alledrogo-postgresql.czcs88g82ce0.us-east-1.rds.amazonaws.com:5432;Database=alledrogo;Username=postgres;Password=Terraform123!" `
    -e FRONTEND_IP="frontend-lb-1412547079.us-east-1.elb.amazonaws.com" `
    alledrogo-backend:latest