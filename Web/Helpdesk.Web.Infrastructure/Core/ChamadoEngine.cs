using Helpdesk.Dominio.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Web.Infrastructure.Core
{
    public class ChamadoEngine
    {
        public static String[] stopWords = { 
            "a", "agora", "ainda", "alguém", "algum", "alguma", "algumas", "alguns", "ampla",
            "amplas", "amplo", "amplos", "ante", "antes", "ao", "aos", "após", "aquela", "aquelas", "aquele",
            "aqueles","aquilo","as", "até", "através", "cada", "coisa", "coisas", "com", "como", "contra", "contudo",
            "da", "daquele", "daqueles", "das", "de","dela","delas","dele","deles","depois", "dessa", "dessas", "desse",
            "desses","desta","destas","deste","deste","destes","deve","devem","devendo","dever","deverá","deverão","deveria",
            "deveriam","devia","deviam","disse","disso","disto","dito","diz","dizem","do","dos","e","é","ela","elas","ele",
            "eles","em","enquanto","entre","era","essa","essas","esse","esses","esta","está","estamos","estão","estas",
            "estava","estavam","estávamos","este","estes","estou","eu","fazendo","fazer","feita","feitas","feito","feitos",
            "foi","for","foram","fosse","fossem","grande","grandes","há","isso","isto","já","la","lá","lhe","lhes","lo",
            "mas","me","mesma","mesmas","mesmo","mesmos","meu","meus","minha","minhas","muita","muitas","muito","muitos",
            "na","não","nas","nem","nenhum","nessa","nessas","nesta","nestas","ninguém","no","nos","nós","nossa","nossas",
            "nosso","nossos","num","numa","nunca","o","os","ou","outra","outras","outro","outros","para","pela","pelas",
            "pelo","pelos","pequena","pequenas","pequeno","pequenos","per","perante","pode","pude","podendo","poder","poderia",
            "poderiam","podia","podiam","pois","por","porém","porque","posso","pouca","poucas","pouco","poucos","primeiro",
            "primeiros","própria","próprias","próprio","próprios","quais","qual","quando","quanto","quantos","que","quem","são",
            "se","seja","sejam","sem","sempre","sendo","será","serão","seu","seus","si","sido","só","sob","sobre","sua","suas",
            "talvez","também","tampouco","te","tem","tendo","tenha","ter","teu","teus","ti","tido","tinha","tinham","toda",
            "todas","todavia","todo","todos","tu","tua","tuas","tudo","última","últimas","último","últimos","um","uma","umas",
            "uns","vendo","ver","vez","vindo","vir","vos","vós" 
        };

        /// <summary>
        /// Calcula a distancia a partir do algoritmo de levenshtein
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static int LevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];
            for (int i = 0; i <= s.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++)
                d[0, j] = j;
            for (int j = 1; j <= t.Length; j++)
                for (int i = 1; i <= s.Length; i++)
                    if (s[i - 1] == t[j - 1])
                        d[i, j] = d[i - 1, j - 1];  //no operation
                    else
                        d[i, j] = Math.Min(Math.Min(
                            d[i - 1, j] + 1,    //a deletion
                            d[i, j - 1] + 1),   //an insertion
                            d[i - 1, j - 1] + 1 //a substitution
                            );
            return d[s.Length, t.Length];
        }

        /// <summary>
        /// Ordena os chamados por ordem de similaridade usando levenshtein
        /// </summary>
        /// <param name="chaves"></param>
        /// <param name="chamados"></param>
        /// <returns></returns>
        public static IList<Solucao> OrderByLevenshtein(string[] chaves, IList<Chamado> chamados, int count)
        {
            return chamados.Select(o =>
                new
                {
                    chamado = o,
                    similaridade = o.Chaves.Split().Where(p => chaves.Any(q => LevenshteinDistance(q, p) <= (q.Length * 30) / 100)).Count()
                })
                .OrderByDescending(o => o.similaridade)
                .GroupBy(o => o.chamado.Solucao.Codigo)
                .Select(o => o.First().chamado.Solucao)
                .Take(count)
                .ToList();
        }
    }
}
