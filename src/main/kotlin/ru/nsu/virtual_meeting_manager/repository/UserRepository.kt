package ru.nsu.virtual_meeting_manager.repository

import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Modifying
import org.springframework.data.jpa.repository.Query
import org.springframework.stereotype.Repository
import ru.nsu.virtual_meeting_manager.model.User
import java.util.Date
import javax.transaction.Transactional

@Repository
interface UserRepository : JpaRepository<User, Int> {

    @Transactional
    @Modifying
    @Query("INSERT INTO users VALUES (DEFAULT, ?1, ?2, ?3, ?4, ?5, ?6)", nativeQuery = true)
    fun registerUser(email: String, password: String, name: String, surname: String, gender: String, birth: Date)

    fun existsByEmail (email: String) : Boolean

    fun getByEmail (email: String) : User
}