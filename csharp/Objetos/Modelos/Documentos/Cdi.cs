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
///     Certificado de Dispensa de Incorporação - CDI (Reservista).
///     Criação : Vovolinux
///     Data    : 28/06/2020
///     Projeto : Objetos genéricos para C#.
/// </summary>

using System;
using static Objetos.Constantes.ConstantesGerais;
using static Objetos.Constantes.EnumForcasArmadas;

namespace Objetos.Modelos.Documentos
{
    public class Cdi
    {
        public string NumeroCdi { get; set; }
        public int ViaCdi { get; set; }
        public DateTime DataDispensa { get; set; }
        public string MotivoDispensa { get; set; }
        public ForcaArmada ForcaArmada { get; set; }

        public Cdi()
        {

        }

        public Cdi(string numeroCdi, int viaCdi, DateTime dataDispensa, string motivoDispensa, ForcaArmada forcaArmada)
        {
            NumeroCdi = numeroCdi;
            ViaCdi = viaCdi;
            DataDispensa = dataDispensa;
            MotivoDispensa = motivoDispensa;
            ForcaArmada = forcaArmada;
        }

        public override string ToString()
        {
            char sep = SeparadorSplit;
            return NumeroCdi
                + sep + ViaCdi.ToString()
                + sep + DataDispensa.ToString()
                + sep + MotivoDispensa
                + sep + (int)ForcaArmada;
        }
    }
}
