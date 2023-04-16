namespace ClubeDaLeitura
{
    public struct Endereco
    {
        public string Rua;
        public int Numero;
        public string Bairro;
        public string Cep;
        public string Complemento;

        public Endereco(string rua, int numero, string bairro, string cep, string complemento)
        {
            this.Rua = rua;
            this.Numero = numero;
            this.Bairro = bairro;
            this.Cep = cep;
            this.Complemento = complemento;
        }

        public override string ToString()
        {
            return $"{Rua,-20} | {Numero,-5} | {Bairro,-20} | {Cep,-10} | {Complemento}";
        }
    }
}