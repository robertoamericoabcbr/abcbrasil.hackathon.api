
ABCBrasil.Hackthon.Api

# 🔢 Versionamento
Para versionamento a API de ABCBrasil.Hackthon.Api busca respeitar o padrão __[SemVer](https://semver.org/lang/pt-BR/)__.
A versão da API é composta por 3 elementos: major, minor e patch. A versão `v[x]`que consta no path da URL é o elemento major da versão da API. A evolução da versão se dá seguinte forma:
- Major: alterações incompatíveis, com quebra de contrato (v1.0.0 → v2.0.0)
- Minor: alterações compatíveis, sem quebra de contrato (v1.1.0 → v1.2.0)
- Patch: correção de bugs mantendo a compatibilidade com a versão anterior. (v1.1.1 → v1.1.2)
Alterações sem quebra de contrato e esclarecimentos às especificações podem ocorrer a qualquer momento. Clientes devem estar preparados para lidar com essas mudanças sem impacto.
Os tipos de mudanças considerados como retrocompatíveis estão listados abaixo:
- Adição de novos campos em resposta.
- Adição de novos parâmetros opcionais.
- Alteração da ordem de campos.
- Adição de novos elementos em enums.
- Adição de novos recursos na API.
 
# 💣 Tratamento de erros
A API usa códigos de resposta HTTP convencionais para indicar o sucesso ou falha de uma solicitação de API. Em geral: Códigos na faixa `2xx` indicam sucesso. Os códigos no intervalo `4xx` indicam um erro que falhou de acordo com as informações fornecidas (por exemplo, um parâmetro obrigatório foi omitido, alguma regra de negócio não foi atendida, etc.). Códigos na faixa `5xx` indicam um erro com os servidores do Banco ABC Brasil.

Atributos do objeto **Error:**

`code` (string) - Código único da mensagem de erro.

`message` (string) - Mensagem amigável amigável do erro.

`param` (string) - Caminho do parâmetro referido.

`paramType` (string | enum) - Tipo do parâmetro referido (body, header, query ou router).

<br/>

# 🚀 Funcionalidades previstas

### DELETE
- `​/api​/v1​/user​/{id}`

### GET
- `​/api​/v1​/user​/{id}`

### POST
- `​/api​/v1/user`

### PUT
- `​/api​/v1​/user​/{id}`

<br/>