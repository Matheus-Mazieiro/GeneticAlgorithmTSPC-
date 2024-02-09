# GeneticAlgorithmTSPC-
A study using Genetic Algorithm to solve TSP with C#, implementing Unit Tests

O Problema do Caixeiro Viajante (TSP) é um clássico problema de otimização combinatória que, dado um conjunto de $n$ cidades, precisa-se encontrar o menor caminho possível que visita todas e retorna a cidade de origem. Sendo o objetivo é minimizar a distância total percorrida pelo caixeiro viajante.

O TSP é conhecido por ser um problema NP-difícil, o que significa que não existe um algoritmo eficiente conhecido para resolver todas as instâncias do problema em tempo polinomial. Em outras palavras, se fossemos testar todas as possibilidades em busca da solução exata mais simples, o custo seria da ordem de $O(n!)$. No entanto, existem várias abordagens heurísticas e algoritmos aproximados que podem fornecer soluções razoáveis em tempo aceitável para muitas instâncias do problema.

Dessa forma, o problema pode ser modelando usando um grafo $G = (V, E)$, onde $V = \{0 .. n-1\}$ representa as cidades.

Nesse projeto apresentaremos uma heurística de algoritmo genético para tentar encontrar a melhor solução.

## Representação da Solução

Representaremos cada indivíduo por um cromossomo $v[0 .. n-1]$ de $n$ alelos, sendo que o $i$-ésimo alelo contém a $i$-ésima cidade que será visitada, dessa forma, em uma instância onde $n$ = 5, possíveis soluções são:

`|1|2|0|3|4|` : Que representa a ordem de visitação 1 -> 2 -> 0 -> 3 -> 4 -> 1

`|0|4|1|3|2|` : Que representa a ordem de visitação 0 -> 4 -> 1 -> 3 -> 2 -> 0

Importante ressaltar que como no problema é necessário que o caixeiro viajante retorne à cidade de origem, então na interpretação do cromossomo deve-se considerar a primeira cidade ao fim da rota, formando assim um ciclo.

## Função Objetivo

A avaliação do indíviduo pode ser calculada através da soma dos custos das distancias dadas pelas cidades

$c(indiv) = v[n-1][0] + \sum_{i=0}^{n-2} (v[i] + v[i+1])$

Onde $v$ é a representação da solução.

Dessa forma, o objetivo é achar $min(c)$


## O Algoritmo

O pseudocódigo do algoritmo pode ser dado por:

```
indiv AG(nIndiv, n, taxaMut, geracoes, nIndivPorGerac):

    populacao = GeraPopulacao(nIndiv, n)
    AvaliaAptidao(populacao)

    Enquanto i < geracoes:
        Enquanto j < nIndivPorGerac:
        {
            p1 = SelecionaPai(populacao)
            p2 = SelecionaPai(populacao)
            filho = Crossover(p1, p2)

            sorteie um valor 𝜸 no intervalo [0, 100]
            Se 𝜸 >= taxaMut:
                Mutacao(filho)
            
            Insira filho na populacao
        }

        AvaliaAptidao(populacao)
        Controle a população

    retorne o melhor individuo
}
```

## População Inicial

A geração inicial consiste em gerar $nIndiv$ indivíduos visitando todos as $n$ cidades em ordem aleatória.

Em pseudocódigo:
```
populacao GeraPopulacao(nIndiv, n):
    pop = {}
    para i = 0 .. nIndiv:
        GeraIndiv indiv(n)
        insira indiv em pop
    retorna pop
```

## Seleção dos Pais

Para selecionar cada pai ($p_1$ e $p_2$) que gerará o filho c, será realizado um torneio binário, onde serão sorteados dois indivíuos $p_a$ e $p_b$, e, baseado em sua função objetivo, ou seja, no custo que a solução representada por cada um dos indivíduos $p_a$ e $p_b$, o melhor será selecionado como pai.

Em um caso de exemplo, foi sorteado os indivíduos:

$p_a$ = `|1|2|0|3|4|`, com custo 22; e

$p_b$ = `|0|4|1|3|2|`, com custo 25.

Logo um dos pais será o indivído `|1|2|0|3|4|` com custo 22, pois $c(p_a) < c(p_b)$ o processo se repete para a seleção do outro pai.


## Crossover

Crossover é o processo de geração de novos indivídos, baseado na reprodução, o indivíduo filho será gerado a partir de 2 pais $p_1$ e $p_2$, seguindo o seguinte algoritmo:

```
filho Crossover(p1, p2):
    sorteie 𝛂 e 𝛃, tal que 𝛂 <= 𝛃
    copie os alelos entre [𝛂, 𝛃] de p1 para o filho
    para as cidades faltantes preencha em filho com base na ordem em p2
```

O algoritmo de crossover acima é baseado em Ha et al. (2019), que aplica outro algoritmo genético para a solução de um problema relacionado (TSP-D).

## Mutação

O operador de mutação ocorre com probabilidade $taxaMut$, esse operador simplismente troca a posição entre 2 alelos de um filho, assim diversificando o espaço de busca e dificultando a diversificação prematura e indo contra a estagnação em ótimos locais.

## Controle da População

A cada geração é necessário manter a população com $nIndiv$ indivíduos, para isso será necessário eliminar os piores com base no custo da solução de cada indivíduo.

---
# Testes Unitários

Serão realizados testes que avaliarão algumas funcionalidades específicas do algoritmo, como por exemplo:
- A geração de soluções válidas para grafos com diferentes tamanhos de vértices;
- A avaliação de custo de um determinado caminho em um gráfo pré-determinado;
- A comparação entre indivíduos específicos em grafos pré-determinados;
- A mutação de indivíduos;
- O resultado final do algoritmo genético em grafos pré-determinados.

Em alguns desses testes é possível determinar objetivamente se foi aprovado ou não, como por exemplo a mutação, entretanto em outros casos existe a necessidade de comparar a resposta com um limiar satisfatório, já que, por se tratar de uma heurística, pode ser que o trabalho não retorne o valor exato, mas algum suficientemente bom.


Os testes foram realizados usando NUnit, e para sua execução basta, no visual studio 2019 com a extensão do NUnit, basta executar todas as tasks em visualização (Ctrl+R, V) na aba Test Explorer.

![image](https://github.com/Matheus-Mazieiro/GeneticAlgorithmTSPC-/assets/37386694/e008a899-766c-4fac-b84e-a69c3fa6945b)


Para isso, garanta que o projeto de testes esteja na mesma solução do projeto do algoritmo genético e que exista uma referência para o projeto do algoritmo genético nas dependências do projeto de testes:
  - Botão direito em "Dependencies" do projeto de testes > Add Project References > seleciona o projeto do algoritmo genético.

![image](https://github.com/Matheus-Mazieiro/GeneticAlgorithmTSPC-/assets/37386694/8aa99d4a-419c-42a1-8f94-624b278a27bd)

![image](https://github.com/Matheus-Mazieiro/GeneticAlgorithmTSPC-/assets/37386694/98a436ee-e4d7-4ccd-8661-9aec137e9a11)
