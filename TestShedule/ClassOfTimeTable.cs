using TestShedule;


namespace TestShedule
{

    public class ClassOfTimetable
    {
        public string idSubject;
        public string idLecture;
        public string idClass;
        public int lesson;
        public int idTime;

        public ClassOfTimetable(string idClass, string idLecture = null, int lesson = 1, string idTime = null, string idSubject = null)
        {
            this.idClass = idClass;
            this.idLecture = idLecture;
            this.lesson = lesson;
            this.idSubject = idSubject;
        }

        public ClassOfTimetable()
        {
            this.idClass = null;
            this.idLecture = null;
            this.lesson = 0;
            this.idSubject = null;
        }

        static public int CompareClass(ClassOfTimetable a, ClassOfTimetable b)
        {
            if (a.idTime < b.idTime)
            {
                return -1;
            }
            else if (a.idTime > b.idTime)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
