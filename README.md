# Modelagem de API

Em um determinado banco, você foi designado a desenhar o novo p processamento de liberação
de crédito. Existem 5 tipos de créditos, sendo:
- Crédito Direto Taxa de 2
- Crédito Consignado Taxa de 1%
- Crédito Pessoa Jurídica Taxa de 5%
- Crédito Pessoa Física Taxa de 3%
- Crédito Imobiliário Taxa de 9%
  
Todos precisarão passar no método de validação abaixo para viabilizar a sua liberação.
- Crie as classes que julgar necessárias para implementação do processamento e demonstre o
funcionamento através de uma aplicação.
- Defina como entradas as seguintes variáveis:
- Valor do crédito;
- Tipo de crédito;
- Quantidade de parcelas;
- Data do primeiro vencimento.
- As validações das entradas
  
As validações das entradas serão as seguintes:
- O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00;
- A quantidade mínima de parcelas é de 5x e máxima de 72x;
- Para o crédito de pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00;
- A data do primeiro vencimento sempre será no mínimo 15 dias e no máximo 40 dias a partir
da data atual.
Os resultados precisam conter as seguintes informações:
- Status do crédito (Aprovado ou recusado, de acordo com as premissas acima);
- Valor total com juros (Os juros serão calculados através do incremento da porcentagem de
juros no valor do crédito);
- Valor dos juros.
