namespace WPFClient
{
    public class Department : NotifyPropertyChanged
    {
        //Id отдела
        private ulong id;
        public ulong Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        //Название отдела
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Department(string name)
        {
            Name = name;
        }

        public Department(ulong id, string name) : this(name)
        {
            Id = id;
        }

    }
}
