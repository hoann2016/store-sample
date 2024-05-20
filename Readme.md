**Run with docker compose**
``docker compose up``

- Request product =>Web ==> Api gateway=>Catalog Api==>Postgres
- Request cart:
    - Get =>WeB==> Api gateway ==> Basket Api ===>Postgres =>reponse Cart
    - Store =>WeB==> Api gateway ==> Basket Api ===>check Discound on GRPC =>Postgres=> Response User Name
    - Checkout=>Web==>Api gateway=>Basket Api=>publish event use MassTransis=>Delete basket =>reponse True
- Request Order:
    - request =>Web =>Api gateway =>Order Api ==>Message brocket (manage cart)=>SQL Server=> return order.
    - BasketCheckoutEventHandler consume the basket checkout=>Sql Server 
- GRPC:
    - contain discount data, ready to accept request to check discount from basket.
    - sqlite   

