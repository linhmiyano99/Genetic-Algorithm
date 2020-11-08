using System;
using System.Collections.Generic;
using System.Text;

namespace GA
{
    public class ClassOfTimetable
    {
        public string idSubject;
        public string idLecture;
        public string idClass;
        public int lesson;

        public ClassOfTimetable(string idClass,  string idLecture = null, int lesson = 1, string idSubject = null)
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
    }
}
