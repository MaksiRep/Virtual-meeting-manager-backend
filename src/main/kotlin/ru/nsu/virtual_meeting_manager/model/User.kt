package ru.nsu.virtual_meeting_manager.model

import java.util.Date
import javax.persistence.Column
import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.Table

@Table(name = "USERS")
@Entity
class User(

    @Id
    @Column(name = "ID")
    val id: Int,

    @Column(name = "E_MAIL")
    var email: String = "",

    @Column(name = "PASSWORD")
    var password: String = "",

    @Column(name = "NAME")
    var name: String = "",

    @Column(name = "SURNAME")
    var surname: String = "",

    @Column(name = "GENDER")
    var gender: String = "",

    @Column(name = "BIRTH")
    var birth: Date,

)