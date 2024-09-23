namespace SWII6.TP01.Negocio
{
    public class Author
    {
        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public char Gender { get { return gender; } set { gender = value; } }

        private string name;
        private string email;
        private char gender;
        public Author() { }
        public Author(string name, string email, char gender)
        {
            Name = name;
            Email = email;
            Gender = gender;
        }
    }
}
