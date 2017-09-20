using System.Linq;

namespace DesignPatterns.Behavioral.Repository
{
	class Student
	{
		private readonly IRepository _repository;

		public Student(IRepository repository)
		{
			_repository = repository;
		}

		public string Name { get; set; }
		public string Address { get; set; }
		public string RollNo { get; set; }
		public string Class { get; set; }

		public bool AddStudent()
		{
			return _repository.Insert(this);
		}
	}

	interface IRepository
	{
		bool Insert(Student student);
		void Delete(Student student);
		void Update(Student student);
		Student GetById(Student RollNo);
		IQueryable<Student> GetAll();
	}

	class SQLRepository : IRepository
	{

		public bool Insert(Student student)
		{
			//Insert logic in sql server db
			return true;
		}

		public void Delete(Student student)
		{
			//logic to delete in sql server db
		}

		public void Update(Student student)
		{
			//logic to update in sql server db
		}

		public Student GetById(Student RollNo)
		{
			//logic to Retreive by id from sql server db
			return null;
		}

		public IQueryable<Student> GetAll()
		{
			//logic to Retreive from sql server db
			return null;
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
		}
	}
}
