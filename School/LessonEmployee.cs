
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace School
{

using System;
    using System.Collections.Generic;
    
public partial class LessonEmployee
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public LessonEmployee()
    {

        this.Schedule = new HashSet<Schedule>();

    }


    public int id { get; set; }

    public int IdLesson { get; set; }

    public int IdEmployees { get; set; }



    public virtual Employee Employee { get; set; }

    public virtual Lesson Lesson { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Schedule> Schedule { get; set; }

}

}
