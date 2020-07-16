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
///     Persistência em arquivo para PessoaJuridica.
///     Criação : Vovolinux
///     Data    : 05/07/2020
///     Projeto : Objetos genéricos para C#.
/// </summary>

using System;
using System.Collections.Generic;
using Objetos.Constantes;
using static Objetos.Controles.ControleMensagem;
using Objetos.Interfaces;
using Objetos.Modelos;
using Objetos.Modelos.Documentos;
using Objetos.Modelos.Enderecos;
using Objetos.Modelos.Pessoas;
using Objetos.Utilitarios;
using static Objetos.Constantes.EnumRegiao;
using static Objetos.Constantes.EnumSituacao;
using static Objetos.Constantes.EnumTipoCnae;
using static Objetos.Constantes.EnumTipoEnderecoTelefoneEmail;
using static Objetos.Constantes.EnumTipoEstabelecimento;
using static Objetos.Constantes.EnumTipoPessoa;
using static Objetos.Constantes.EnumVinculoPessoa;
using static Objetos.Constantes.ConstantesGerais;
using Objetos.Controles;

namespace Objetos.Persistencia.Arquivos
{
    public class PAPessoaJuridica : ICRUD<PessoaJuridica>
    {
        #region ATRIBUTOS

        private Arquivo controleArquivo = null;

        private PessoaJuridica pessoa = null;
        private List<PessoaJuridica> pessoas = null;
        private List<PessoaJuridica> pessoasRetorno = null;

        #endregion ATRIBUTOS

        #region CONSTRUTORES

        public PAPessoaJuridica()
        {
            controleArquivo = new Arquivo("PessoaJuridica", ExtensaoArquivoBd, "");
        }

        #endregion CONSTRUTORES

        #region CREATE
        public long Incluir(PessoaJuridica pessoaJuridica)
        {
            try
            {
                pessoaJuridica.IdPessoa = GeradorID.getProximoID();
                controleArquivo.IncluirLinha(pessoaJuridica.ToString());
                return pessoaJuridica.IdPessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "001" + SeparadorEnter + "Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " + MensagemCompleta(ex.Message));
            }
        }
        #endregion CREATE

        #region READ
        public PessoaJuridica Buscar(long idPessoa)
        {
            try
            {
                foreach (PessoaJuridica pessoa in Consultar())
                    if (pessoa.IdPessoa == idPessoa)
                        return pessoa;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "002" + SeparadorEnter + "Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " + MensagemCompleta(ex.Message));
            }
        }

        public List<PessoaJuridica> Consultar()
        {
            try
            {
                pessoas = new List<PessoaJuridica>();
                string[] linhas = controleArquivo.LerLinhas();

                foreach (string linha in linhas)
                    pessoas.Add(ToObject(linha));

                return pessoas;
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "003" + SeparadorEnter + "Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " + MensagemCompleta(ex.Message));
            }
        }

        public List<PessoaJuridica> Consultar(object parametro, string atributo)
        {
            try
            {
                pessoas = Consultar();
                pessoasRetorno = new List<PessoaJuridica>();

                // Retorna vazio se não informar o atributo
                if (atributo.Trim().Length == 0)
                    return pessoasRetorno;

                foreach (PessoaJuridica pessoaJuridica in pessoas)
                {
                    switch (atributo)
                    {
                        case "TipoPessoa":
                            if (pessoaJuridica.TipoPessoa.Equals((TipoPessoa)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "SituacaoPessoa":
                            if (pessoaJuridica.SituacaoPessoa.Equals((Situacao)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "Vinculo":
                            if (pessoaJuridica.Vinculo.Equals((Vinculo)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "Cnpj":
                            if (pessoaJuridica.Cnpj.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "TipoEstabelecimento":
                            if (pessoaJuridica.TipoEstabelecimento.Equals((TipoEstabelecimento)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "DataAbertura":
                            if (pessoaJuridica.DataAbertura.Equals((DateTime)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "NomeEmpresarial":
                            if (pessoaJuridica.NomeEmpresarial.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "NomeFantasia":
                            if (pessoaJuridica.NomeFantasia.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "PortePJ":
                            if (pessoaJuridica.PortePJ.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "CodigoNaturezaJuridica":
                            if (pessoaJuridica.CodigoNaturezaJuridica.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "DescricaoNaturezaJuridica":
                            if (pessoaJuridica.DescricaoNaturezaJuridica.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "Efr":
                            if (pessoaJuridica.Efr.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "SituacaoCnpj":
                            if (pessoaJuridica.SituacaoCnpj.Equals((Situacao)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "DataSituacaoCadastral":
                            if (pessoaJuridica.DataSituacaoCadastral.Equals((DateTime)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "MotivoSituacaoCadastral":
                            if (pessoaJuridica.MotivoSituacaoCadastral.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "SituacaoEspecial":
                            if (pessoaJuridica.SituacaoEspecial.Equals((string)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        case "DataSituacaoEspecial":
                            if (pessoaJuridica.DataSituacaoEspecial.Equals((DateTime)parametro))
                                pessoasRetorno.Add(pessoaJuridica);

                            break;

                        default:
                            break;
                    }
                }

                return pessoasRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "004" + SeparadorEnter + "Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " + MensagemCompleta(ex.Message));
            }
        }

        public PessoaJuridica ToObject(string texto)
        {
            try
            {
                char sep = SeparadorSplit;
                string[] partes = texto.Split(SeparadorSplit);

                PessoaJuridica pj = new PessoaJuridica(
                    long.Parse(partes[0]),
                    (Situacao)int.Parse(partes[2]),
                    (Vinculo)int.Parse(partes[3]),
                    partes[4],
                    (TipoEstabelecimento)int.Parse(partes[5]),
                    DateTime.Parse(partes[6]),
                    partes[7],
                    partes[8],
                    partes[9],
                    partes[10],
                    partes[11],
                    partes[12],
                    (Situacao)int.Parse(partes[13]),
                    DateTime.Parse(partes[14]),
                    partes[15],
                    partes[16],
                    DateTime.Parse(partes[17])
                    );

                // Enderecos
                pj.Enderecos = new ControleEndereco().Consultar(pj.IdPessoa, "IdPessoa");
                // Telefones
                pj.Telefones = new ControleTelefone().Consultar(pj.IdPessoa, "IdPessoa");
                // Emails
                pj.Emails = new ControleEmail().Consultar(pj.IdPessoa, "IdPessoa");

                return pessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "005" + SeparadorEnter + "Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " 
                    + MensagemCompleta(ex.Message));
            }
        }

        #endregion READ

        #region UPDATE
        public void Atualizar(PessoaJuridica pessoaJuridica)
        {
            try
            {
                foreach (PessoaJuridica pessoa in Consultar())
                    if (pessoa.IdPessoa == pessoaJuridica.IdPessoa)
                    {
                        controleArquivo.SubstituirLinha(pessoa.ToString(), pessoaJuridica.ToString());
                        break;
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "006" + SeparadorEnter + "Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " + MensagemCompleta(ex.Message));
            }
        }

        #endregion UPDATE

        #region DELETE

        public void Excluir(long idPessoa)
        {
            try
            {
                foreach (PessoaJuridica pessoa in Consultar())
                    if (pessoa.IdPessoa == idPessoa)
                    {
                        controleArquivo.ExcluirLinha(pessoa.ToString());
                        break;
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("pej" + SeparadorTraco + "007Camada: Persistência-Arquivos" + SeparadorEnter + "Erro: " + MensagemCompleta(ex.Message));
            }
        }

        #endregion DELETE
    }
}
