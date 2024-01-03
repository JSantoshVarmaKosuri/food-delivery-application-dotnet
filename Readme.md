// run the application with below command
dotnet run --project=.\services\fdorder\fdorder.csproj

//Update the connection string in OrderContext.cs

curl -X 'POST' \
  'http://localhost:5185/service/Order/Create' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "restarentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderItems": [
    {
      "orderItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "dishes": [
        {
          "dishId": "00ffb36c-3644-41c9-8309-feb0870ab786",
          "orderItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "name": "Tomatoes - Vine Ripe, Red",
          "price": 416.08,
          "quantity": 2
        },
        {
          "dishId": "bfb9afc2-cba7-4552-be7f-cb54b20d08f4",
          "orderItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "name": "Chicken - Thigh, Bone In",
          "price": 293.45,
          "quantity": 5
        }
      ],
      "subTotal": 0
    },
    {
      "orderItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "dishes": [
        {
          "dishId": "f7f5024d-56b7-4250-876e-11f6d8b2d661",
          "orderItemId": "49b3e33e-c751-491b-a729-87dfb4ba08cd",
          "name": "Bread - Crusty Italian Poly",
          "price": 366.79,
          "quantity": 4
        },
        {
          "dishId": "1f934671-6ee9-4030-9bcd-53d4c7223cb1",
          "orderItemId": "fabf0ce7-b7ea-439c-95cd-0accbb341fc2",
          "name": "Pepper - Red Bell",
          "price": 686.2,
          "quantity": 4
        }
      ],
      "subTotal": 0
    }
  ]
}'

Should insert a record in mySql database