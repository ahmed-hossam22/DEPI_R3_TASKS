using OOP_Project_1;
using System;

ExaminationSystem system = new ExaminationSystem();

Console.WriteLine("Welcome to Examination System!");
Console.WriteLine("==============================");

bool running = true;
while (running)
{
    ShowMainMenu();
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ManageCourses();
            break;
        case "2":
            ManageStudents();
            break;
        case "3":
            ManageInstructors();
            break;
        case "4":
            ManageExams();
            break;
        case "5":
            TakeExamMenu();
            break;
        case "6":
            ViewResults();
            break;
        case "7":
            CompareStudentsMenu();
            break;
        case "8":
            running = false;
            Console.WriteLine("Thank you for using Examination System!");
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }

    if (running)
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

void ShowMainMenu()
{
    Console.WriteLine("\n=== Main Menu ===");
    Console.WriteLine("1. Manage Courses");
    Console.WriteLine("2. Manage Students");
    Console.WriteLine("3. Manage Instructors");
    Console.WriteLine("4. Manage Exams");
    Console.WriteLine("5. Take Exam");
    Console.WriteLine("6. View Results");
    Console.WriteLine("7. Compare Students");
    Console.WriteLine("8. Exit");
    Console.Write("Choose an option: ");
}

void ManageCourses()
{
    Console.WriteLine("\n=== Course Management ===");
    Console.WriteLine("1. Add Course");
    Console.WriteLine("2. View All Courses");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        AddCourse();
    }
    else if (choice == "2")
    {
        ViewAllCourses();
    }
}

void AddCourse()
{
    Console.Write("Enter Course ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Enter Course Title: ");
    string title = Console.ReadLine();

    Console.Write("Enter Course Description: ");
    string description = Console.ReadLine();

    Console.Write("Enter Maximum Degree: ");
    int maxDegree = int.Parse(Console.ReadLine());

    Course course = new Course(id, title, description, maxDegree);
    system.AddCourse(course);
}

void ViewAllCourses()
{
    Console.WriteLine("\n=== All Courses ===");
    if (system.Courses.Count == 0)
    {
        Console.WriteLine("No courses found!");
        return;
    }

    foreach (Course course in system.Courses)
    {
        Console.WriteLine(course.ToString());
        Console.WriteLine($"Description: {course.Description}");
        Console.WriteLine("------------------");
    }
}

void ManageStudents()
{
    Console.WriteLine("\n=== Student Management ===");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. View All Students");
    Console.WriteLine("3. Enroll Student in Course");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        AddStudent();
    }
    else if (choice == "2")
    {
        ViewAllStudents();
    }
    else if (choice == "3")
    {
        EnrollStudentInCourse();
    }
}

void AddStudent()
{
    Console.Write("Enter Student ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Enter Student Name: ");
    string name = Console.ReadLine();

    Console.Write("Enter Student Email: ");
    string email = Console.ReadLine();

    Student student = new Student(id, name, email);
    system.AddStudent(student);
}

void ViewAllStudents()
{
    Console.WriteLine("\n=== All Students ===");
    if (system.Students.Count == 0)
    {
        Console.WriteLine("No students found!");
        return;
    }

    foreach (Student student in system.Students)
    {
        Console.WriteLine(student.ToString());
        Console.WriteLine($"Enrolled Courses: {student.EnrolledCourses.Count}");
        Console.WriteLine("------------------");
    }
}

void EnrollStudentInCourse()
{
    if (system.Students.Count == 0 || system.Courses.Count == 0)
    {
        Console.WriteLine("Please add students and courses first!");
        return;
    }

    Console.Write("Enter Student ID: ");
    int studentId = int.Parse(Console.ReadLine());

    Console.Write("Enter Course ID: ");
    int courseId = int.Parse(Console.ReadLine());

    Student student = system.FindStudentById(studentId);
    Course course = system.FindCourseById(courseId);

    if (student != null && course != null)
    {
        student.EnrollInCourse(course);
    }
    else
    {
        Console.WriteLine("Student or Course not found!");
    }
}

void ManageInstructors()
{
    Console.WriteLine("\n=== Instructor Management ===");
    Console.WriteLine("1. Add Instructor");
    Console.WriteLine("2. View All Instructors");
    Console.WriteLine("3. Assign Instructor to Course");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        AddInstructor();
    }
    else if (choice == "2")
    {
        ViewAllInstructors();
    }
    else if (choice == "3")
    {
        AssignInstructorToCourse();
    }
}

void AddInstructor()
{
    Console.Write("Enter Instructor ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Enter Instructor Name: ");
    string name = Console.ReadLine();

    Console.Write("Enter Specialization: ");
    string specialization = Console.ReadLine();

    Instructor instructor = new Instructor(id, name, specialization);
    system.AddInstructor(instructor);
}

void ViewAllInstructors()
{
    Console.WriteLine("\n=== All Instructors ===");
    if (system.Instructors.Count == 0)
    {
        Console.WriteLine("No instructors found!");
        return;
    }

    foreach (Instructor instructor in system.Instructors)
    {
        Console.WriteLine(instructor.ToString());
        Console.WriteLine($"Teaching Courses: {instructor.TeachingCourses.Count}");
        Console.WriteLine("------------------");
    }
}

void AssignInstructorToCourse()
{
    if (system.Instructors.Count == 0 || system.Courses.Count == 0)
    {
        Console.WriteLine("Please add instructors and courses first!");
        return;
    }

    Console.Write("Enter Instructor ID: ");
    int instructorId = int.Parse(Console.ReadLine());

    Console.Write("Enter Course ID: ");
    int courseId = int.Parse(Console.ReadLine());

    Instructor instructor = system.FindInstructorById(instructorId);
    Course course = system.FindCourseById(courseId);

    if (instructor != null && course != null)
    {
        instructor.AssignToCourse(course);
    }
    else
    {
        Console.WriteLine("Instructor or Course not found!");
    }
}

void ManageExams()
{
    Console.WriteLine("\n=== Exam Management ===");
    Console.WriteLine("1. Create New Exam");
    Console.WriteLine("2. View All Exams");
    Console.WriteLine("3. Add Questions to Exam");
    Console.WriteLine("4. Copy Exam to Another Course");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            CreateNewExam();
            break;
        case "2":
            ViewAllExams();
            break;
        case "3":
            AddQuestionsToExam();
            break;
        case "4":
            CopyExamToCourse();
            break;
    }
}

void CreateNewExam()
{
    if (system.Courses.Count == 0)
    {
        Console.WriteLine("Please add courses first!");
        return;
    }

    Console.Write("Enter Exam Title: ");
    string title = Console.ReadLine();

    Console.Write("Enter Exam Time (minutes): ");
    int time = int.Parse(Console.ReadLine());

    Console.WriteLine("Available Courses:");
    for (int i = 0; i < system.Courses.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {system.Courses[i].Title}");
    }

    Console.Write("Choose Course (number): ");
    int courseChoice = int.Parse(Console.ReadLine()) - 1;

    if (courseChoice >= 0 && courseChoice < system.Courses.Count)
    {
        Course selectedCourse = system.Courses[courseChoice];

        Console.WriteLine("Exam Type:");
        Console.WriteLine("1. Final Exam");
        Console.WriteLine("2. Practical Exam");
        Console.Write("Choose type: ");

        string examType = Console.ReadLine();
        Exam_Base exam;

        if (examType == "1")
        {
            exam = new Final_Exam(title, time, selectedCourse);
        }
        else
        {
            exam = new Practical_Exam(title, time, selectedCourse);
        }

        system.AddExam(exam);
    }
    else
    {
        Console.WriteLine("Invalid course selection!");
    }
}

void ViewAllExams()
{
    Console.WriteLine("\n=== All Exams ===");
    if (system.Exams.Count == 0)
    {
        Console.WriteLine("No exams found!");
        return;
    }

    for (int i = 0; i < system.Exams.Count; i++)
    {
        Console.WriteLine($"\nExam {i + 1}:");
        system.Exams[i].Show_Exam_Info();
        Console.WriteLine("------------------");
    }
}

void AddQuestionsToExam()
{
    if (system.Exams.Count == 0)
    {
        Console.WriteLine("No exams found! Create an exam first.");
        return;
    }

    Console.WriteLine("Available Exams:");
    for (int i = 0; i < system.Exams.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {system.Exams[i].Title}");
    }

    Console.Write("Choose Exam (number): ");
    int examChoice = int.Parse(Console.ReadLine()) - 1;

    if (examChoice >= 0 && examChoice < system.Exams.Count)
    {
        Exam_Base selectedExam = system.Exams[examChoice];

        if (selectedExam.IsStarted)
        {
            Console.WriteLine("Cannot add questions to a started exam!");
            return;
        }

        Console.WriteLine("\nQuestion Types:");
        Console.WriteLine("1. Multiple Choice Question");
        Console.WriteLine("2. True/False Question");
        Console.WriteLine("3. Essay Question");
        Console.Write("Choose question type: ");

        string questionType = Console.ReadLine();

        Console.Write("Enter Question Header: ");
        string header = Console.ReadLine();

        Console.Write("Enter Question Body: ");
        string body = Console.ReadLine();

        Console.Write("Enter Question Marks: ");
        int marks = int.Parse(Console.ReadLine());

        Question_Base question = null;

        switch (questionType)
        {
            case "1":
                question = CreateMCQQuestion(header, body, marks);
                break;
            case "2":
                question = CreateTrueFalseQuestion(header, body, marks);
                break;
            case "3":
                question = new Essay(header, body, marks);
                break;
            default:
                Console.WriteLine("Invalid question type!");
                return;
        }

        if (question != null)
        {
            selectedExam.AddQuestion(question);
        }
    }
    else
    {
        Console.WriteLine("Invalid exam selection!");
    }
}

MCQ CreateMCQQuestion(string header, string body, int marks)
{
    Console.Write("Enter number of choices: ");
    int numChoices = int.Parse(Console.ReadLine());

    Answer[] answers = new Answer[numChoices];

    for (int i = 0; i < numChoices; i++)
    {
        Console.Write($"Enter choice {i + 1}: ");
        string choiceText = Console.ReadLine();
        answers[i] = new Answer(i + 1, choiceText);
    }

    Console.Write("Enter correct answer number (1-" + numChoices + "): ");
    int correctAnswer = int.Parse(Console.ReadLine());

    return new MCQ(header, body, marks, answers, correctAnswer);
}

True_False CreateTrueFalseQuestion(string header, string body, int marks)
{
    Console.WriteLine("Is the statement True or False?");
    Console.WriteLine("1. True");
    Console.WriteLine("2. False");
    Console.Write("Enter correct answer (1 or 2): ");

    int answer = int.Parse(Console.ReadLine());
    bool isTrue = answer == 1;

    return new True_False(header, body, marks, isTrue);
}

void CopyExamToCourse()
{
    if (system.Exams.Count == 0 || system.Courses.Count == 0)
    {
        Console.WriteLine("Please add exams and courses first!");
        return;
    }

    Console.WriteLine("Available Exams:");
    for (int i = 0; i < system.Exams.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {system.Exams[i].Title}");
    }

    Console.Write("Choose Exam to copy (number): ");
    int examChoice = int.Parse(Console.ReadLine()) - 1;

    Console.WriteLine("Available Courses:");
    for (int i = 0; i < system.Courses.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {system.Courses[i].Title}");
    }

    Console.Write("Choose destination Course (number): ");
    int courseChoice = int.Parse(Console.ReadLine()) - 1;

    if (examChoice >= 0 && examChoice < system.Exams.Count &&
        courseChoice >= 0 && courseChoice < system.Courses.Count)
    {
        Exam_Base originalExam = system.Exams[examChoice];
        Course targetCourse = system.Courses[courseChoice];

        Exam_Base copiedExam = originalExam.CreateCopy(targetCourse);
        system.AddExam(copiedExam);
    }
    else
    {
        Console.WriteLine("Invalid selection!");
    }
}

void TakeExamMenu()
{
    if (system.Students.Count == 0 || system.Exams.Count == 0)
    {
        Console.WriteLine("Please add students and exams first!");
        return;
    }

    Console.WriteLine("Available Students:");
    for (int i = 0; i < system.Students.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {system.Students[i].Name}");
    }

    Console.Write("Choose Student (number): ");
    int studentChoice = int.Parse(Console.ReadLine()) - 1;

    Console.WriteLine("Available Exams:");
    for (int i = 0; i < system.Exams.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {system.Exams[i].Title} - {system.Exams[i].Course.Title}");
    }

    Console.Write("Choose Exam (number): ");
    int examChoice = int.Parse(Console.ReadLine()) - 1;

    if (studentChoice >= 0 && studentChoice < system.Students.Count &&
        examChoice >= 0 && examChoice < system.Exams.Count)
    {
        Student student = system.Students[studentChoice];
        Exam_Base exam = system.Exams[examChoice];

        // Check if student is enrolled in the course
        bool isEnrolled = false;
        foreach (Course course in student.EnrolledCourses)
        {
            if (course.Id == exam.Course.Id)
            {
                isEnrolled = true;
                break;
            }
        }

        if (!isEnrolled)
        {
            Console.WriteLine("Student is not enrolled in this course!");
            return;
        }

        system.TakeExam(student, exam);
    }
    else
    {
        Console.WriteLine("Invalid selection!");
    }
}

void ViewResults()
{
    Console.WriteLine("\n=== View Results ===");
    if (system.Results.Count == 0)
    {
        Console.WriteLine("No results found!");
        return;
    }

    system.DisplayAllResults();
}

void CompareStudentsMenu()
{
    if (system.Results.Count < 2)
    {
        Console.WriteLine("Need at least 2 results to compare!");
        return;
    }

    Console.WriteLine("Available Results:");
    for (int i = 0; i < system.Results.Count; i++)
    {
        ExamResult result = system.Results[i];
        Console.WriteLine($"{i + 1}. {result.Student.Name} - {result.Exam.Title} - Score: {result.Score}");
    }

    Console.Write("Choose first result (number): ");
    int result1Choice = int.Parse(Console.ReadLine()) - 1;

    Console.Write("Choose second result (number): ");
    int result2Choice = int.Parse(Console.ReadLine()) - 1;

    if (result1Choice >= 0 && result1Choice < system.Results.Count &&
        result2Choice >= 0 && result2Choice < system.Results.Count)
    {
        ExamResult.CompareStudents(system.Results[result1Choice], system.Results[result2Choice]);
    }
    else
    {
        Console.WriteLine("Invalid selection!");
    }
}
