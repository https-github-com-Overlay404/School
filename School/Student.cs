
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
    
public partial class Student
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Student()
    {

        this.VisitLeson = new HashSet<VisitLeson>();

    }


    public int id { get; set; }

    public int IdClass { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Lastname { get; set; }

    public int Login { get; set; }

    public string Password { get; set; }



    public virtual Class Class { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<VisitLeson> VisitLeson { get; set; }

}

}
