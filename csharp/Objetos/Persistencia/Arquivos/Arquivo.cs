﻿/// <licenca>
///     Licença MIT
///     Copyright(c) 2020 Viniciusalopes Tecnologia
///     
///     A permissão é concedida, gratuitamente, a qualquer pessoa que obtenha uma cópia deste software e dos 
///     arquivos de documentação associados (o "Software"), para negociar no Software sem restrições, 
///     incluindo, sem limitação, os direitos de uso, cópia, modificação, fusão, publicar, distribuir, 
///     sublicenciar e/ou vender cópias do Software e permitir que as pessoas a quem o Software é fornecido 
///     o façam, sob as seguintes condições:
///     
///     O aviso de direitos autorais acima e este aviso de permissão devem ser incluídos em todas as cópias 
///     ou partes substanciais do Software.
///     
///     O SOFTWARE É FORNECIDO "TAL COMO ESTÁ", SEM GARANTIA DE QUALQUER TIPO, EXPRESSA OU IMPLÍCITA, 
///     INCLUINDO MAS NÃO SE LIMITANDO A GARANTIAS DE COMERCIALIZAÇÃO, ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA 
///     E NÃO INFRAÇÃO. EM NENHUM CASO OS AUTORES OU TITULARES DE DIREITOS AUTORAIS SERÃO RESPONSÁVEIS POR 
///     QUALQUER REIVINDICAÇÃO, DANOS OU OUTRA RESPONSABILIDADE, SEJA EM AÇÃO DE CONTRATO, TORT OU OUTRA 
///     FORMA, PROVENIENTE, FORA OU EM CONEXÃO COM O SOFTWARE OU O USO, OU OUTROS ACORDOS NOS PROGRAMAS.
/// </licenca>
/// <summary>
///     Modelo de arquivo em disco.
///     Criação : Vovolinux
///     Data    : 30/06/2020
///     Projeto : Objetos genéricos para C#.
/// </summary>

using static Objetos.Controles.ControleMensagem;
using Objetos.Constantes;
using System;
using System.IO;
using System.Text;
using static Objetos.Utilitarios.ArquivoUtils;

namespace Objetos.Persistencia.Arquivos
{
    public class Arquivo
    {
        #region ATRIBUTOS

        private string caminhoArquivo = "";
        private ConfiguracaoPA cfg = null;

        #endregion ATRIBUTOS

        #region CONSTRUTORES

        public Arquivo()
        {

        }

        public Arquivo(string caminhoCompleto)
        {
            try
            {
                garantirArquivo(caminhoCompleto);
                this.caminhoArquivo = caminhoCompleto;
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "001#Camada: Persistência-Arquivos#caminhoCompleto: " + caminhoCompleto
                    + "#" + MensagemCompleta(ex.Message));
            }
        }

        public Arquivo(string diretorioArquivo, string nomeArquivo)
        {
            try
            {
                garantirDiretorio(diretorioArquivo);
                garantirArquivo(diretorioArquivo + nomeArquivo);
                caminhoArquivo = diretorioArquivo + nomeArquivo;
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "002#Camada: Persistência-Arquivos#diretorioArquivo: " + diretorioArquivo
                    + "#nomeArquivo: " + nomeArquivo
                    + "#" + MensagemCompleta(ex.Message));
            }
        }

        public Arquivo(string objeto, string extensao, string dirRoot = "")
        {
            try
            {
                cfg = new ConfiguracaoPA(objeto, extensao, dirRoot);
                garantirDiretorio(cfg.Diretorios.DirDados);
                caminhoArquivo = cfg.Diretorios.DirDados + cfg.Arquivos.ArquivoDeDados;
                garantirArquivo(caminhoArquivo);
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "009#Camada: Persistência-Arquivos#diretorioArquivo: " + cfg.Diretorios.DirDados
                    + "#nomeArquivo: " + cfg.Arquivos.ArquivoDeDados
                    + "#" + MensagemCompleta(ex.Message));
            }


        }
        #endregion CONSTRUTORES

        #region GET / SET
        public string CaminhoArquivo
        {
            get { return caminhoArquivo; }
            set
            {
                try
                {
                    garantirArquivo(value);
                    caminhoArquivo = value;
                }
                catch (Exception ex)
                {
                    throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "003#Camada: Persistência-Arquivos#"
                        + MensagemCompleta(ex.Message));
                }
            }
        }

        #endregion GET / SET

        #region LEITOR

        public string[] LerLinhas()
        {
            try
            {
                garantirArquivo(caminhoArquivo);
                try
                {
                    return File.ReadAllLines(caminhoArquivo, Encoding.UTF8);
                }
                catch (Exception)
                {
                    return new string[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "004#Camada: Persistência-Arquivos#caminhoArquivo: " + caminhoArquivo 
                    + "#" + MensagemCompleta(ex.Message));
            }
        }

        #endregion LEITOR


        #region ESCRITOR

        public void EscreverLinhas(string[] linhas)
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                    File.Delete(caminhoArquivo);

                // Converte o Array em string
                string texto = "";
                foreach (string linha in linhas)
                    texto += linha + Environment.NewLine;

                StreamWriter sw = File.CreateText(caminhoArquivo);
                sw.WriteLine(texto);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "005#Camada: Persistência-Arquivos#caminhoArquivo: " + caminhoArquivo 
                    + "#" + MensagemCompleta(ex.Message));
            }
        }

        public void IncluirLinha(string texto)
        {
            try
            {
                garantirArquivo(caminhoArquivo);
                StreamWriter sw = File.AppendText(caminhoArquivo);
                sw.WriteLine(texto);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "006#Camada: Persistência-Arquivos#caminhoArquivo: " + caminhoArquivo
                    + "#texto: " + texto
                    + "#" + MensagemCompleta(ex.Message)
                    );
            }
        }

        public void SubstituirLinha(string referencia, string novoValor)
        {
            try
            {
                string[] linhas = LerLinhas();
                string[] novasLinhas = new string[linhas.Length];

                for (int i = 0; i < linhas.Length; i++)
                {
                    novasLinhas[i] = (linhas[i].Contains(referencia)) ? (referencia + novoValor) : linhas[i];
                }

                File.WriteAllLines(caminhoArquivo, novasLinhas);
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "007#Camada: Persistência-Arquivos#caminhoArquivo: " + caminhoArquivo
                    + "#referencia: " + referencia
                    + "#novoValor: " + novoValor
                    + "#" + MensagemCompleta(ex.Message)
                    );
            }
        }

        public void ExcluirLinha(string linha)
        {
            try
            {
                string[] linhas = LerLinhas();
                string[] novasLinhas = new string[linhas.Length];

                for (int i = 0; i < linhas.Length; i++)
                    if (!linhas[i].Equals(linha))
                        novasLinhas[i] = linha;

                File.WriteAllLines(caminhoArquivo, novasLinhas);
            }
            catch (Exception ex)
            {
                throw new Exception("arq" + ConstantesGerais.SeparadorTraco + "008#Camada: Persistencia-Arquivos#caminhoArquivo: " + caminhoArquivo
                    + "#linha: " + linha
                    + "#" + MensagemCompleta(ex.Message)
                    );
            }
        }

        public void ExcluirArquivo()
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                    File.Delete(caminhoArquivo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion ESCRITOR

        #region VALIDAÇÃO

        private void garantirDiretorio(string caminho)
        {
            validarNomeDiretorio(caminho);
            criarDiretorio(caminho);
        }

        private void garantirArquivo(string caminhoArquivo)
        {
            validarNomeArquivo(caminhoArquivo);
            criarArquivo(caminhoArquivo);
        }

        #endregion VALIDAÇÃO
    }
}
