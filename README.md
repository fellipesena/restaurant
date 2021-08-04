# restauranteapi

# Ideia:

A ideia é uma API de um Restaurante, onde é possível ter o controle de estoque dos itens, abertura e fechamento de contas e também o controle financeiro dos pedidos.

Para isso, alguns requisitos foram levantados:
- Garçons: Adicionar, remover e visualizar todos;
- Mesas: Adicionar, remover, visualizar todas, visualizar pelo número e visualizar todas disponíveis;
- Item: Adicionar, remover, atualizar, visualizar todos e visualizar pelo ID;
- Conta: Adicionar (iniciar), atualizar (finalizar), visualizar pelo número da mesa;
- Pedido: Adicionar (iniciar pedido);

O passo a passo do funcionamento é:
- Criar um ou mais item, mesa e garçom;
- Iniciar uma nova conta para uma mesa;
- Efetuar um pedido utilizando os itens criados;

Vale ressaltar que há controle de estoque, ou seja, ao efetuar um pedido, a quantidade de itens no estoque diminui, e caso não tenha mais em estoque, o pedido não é realizado. Outro ponto importante é a disponibilidade da mesa, caso tenha uma conta em aberto para uma mesa, a mesma é dada como indisponível, ou seja, não é possível ter duas contas para uma mesma mesa no mesmo tempo.

O valor do item no momento do pedido é armazenado, e caso o mesmo item sofra um ajuste de preço em outro momento, o valor unitário utilizado para o fechamento da conta é o mesmo do momento em que foi realizado o pedido, fazendo com que não hajam surpresas no valor total do fechamento da conta.

O banco de dados foi criado utilizando SQL, o arquivo para criação está em Database/create_database.sql
