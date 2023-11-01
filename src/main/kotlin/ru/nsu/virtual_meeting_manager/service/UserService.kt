package ru.nsu.virtual_meeting_manager.service

import org.springframework.stereotype.Service
import ru.nsu.virtual_meeting_manager.controller.request.UserRequest
import ru.nsu.virtual_meeting_manager.exception.AlreadyExistException
import ru.nsu.virtual_meeting_manager.exception.IncorrectValueException
import ru.nsu.virtual_meeting_manager.repository.UserRepository
import java.nio.charset.Charset
import java.text.ParseException
import java.text.SimpleDateFormat
import java.util.*
import javax.crypto.Cipher
import javax.crypto.spec.IvParameterSpec
import javax.crypto.spec.SecretKeySpec


@Service
class UserService(val userRepository: UserRepository) {

    private val key: String = "ds18fDW5e21jKp0Y"

    fun register(userRequest: UserRequest) {
        if (userRequest.email == "") {
            throw IncorrectValueException("Email cannot be null")
        }
        if (userRequest.name == "") {
            throw IncorrectValueException("Name cannot be null")
        }
        if (userRequest.password == "") {
            throw IncorrectValueException("Password cannot be null")
        }
        if (userRequest.gender == "") {
            throw IncorrectValueException("Gender cannot be null")
        }
        if (userRequest.birth == "") {
            throw IncorrectValueException("Birth cannot be null")
        }
        if (userRepository.existsByEmail(userRequest.email))
            throw AlreadyExistException("User with this email already exist")
        checkCorrectValues(userRequest)
        val formatter = SimpleDateFormat("dd-MM-yyyy")
        var birthDate: Date;
        try {
            birthDate = formatter.parse(userRequest.birth)
            if (birthDate < formatter.parse("01-01-1908") ||
                birthDate > formatter.parse("01-01-2009")
            )
                throw IncorrectValueException("Birth date must be more than 01-01-1908 and less than 01-01-2009")
        } catch (ex: ParseException) {
            throw IncorrectValueException("Incorrect birth type")
        }
        userRepository.registerUser(
            userRequest.email,
            encrypt(userRequest.password),
            userRequest.name,
            userRequest.surname,
            userRequest.gender,
            birthDate
        )
    }

    private fun checkCorrectValues(userRequest: UserRequest) {
        if (userRequest.email.length > 128)
            throw IncorrectValueException("Email must be less than 64 symbols")
        isCorrectNameSurname(userRequest.name)
        isCorrectNameSurname(userRequest.surname)
        isCorrectPassword(userRequest.password)
        if (userRequest.gender != "Муж" && userRequest.gender != "Жен")
            throw IncorrectValueException("Gender must be \'Муж\' or \'Жен\'")

    }

    private fun isCorrectNameSurname(name: String) {
        if (name.length > 128)
            throw IncorrectValueException("Name and Surname must be less than 128 symbols")
        isCorrectSymbols(name)
    }

    private fun isCorrectPassword(password: String) {
        if (password.length < 8)
            throw IncorrectValueException("Password should be more than 7 symbols")
        if (password.length > 64)
            throw IncorrectValueException("Password must be less than 64 symbols")
        if (!password.contains("A-Z".toRegex()))
            throw IncorrectValueException("Password should contains one A-Z symbol")
        isCorrectSymbols(password)
    }

    private fun isCorrectSymbols(symbols: String) {
        for (i in 0 until symbols.length) {
            val char = symbols[i]
            if (char !in 'A'..'Z' && char !in 'a'..'z' && char !in '0'..'9'
                && char !in "[!\"#$%&'()*+,-./:;\\\\<=>?@\\[\\]^_`{|}~]"
            ) {
                throw IncorrectValueException("Symbols shoul be in A-Za-z0-9[!\"#\$%&'()*+,-./:;\\\\<=>?@\\[\\]^_`{|}~]")
            }
        }
    }

    private fun encrypt(input: String): String {
        val cipher = Cipher.getInstance("AES/CBC/PKCS5Padding")
        val key = SecretKeySpec(key.toByteArray(charset("UTF-8")), "AES")
        cipher.init(Cipher.ENCRYPT_MODE, key, IvParameterSpec(ByteArray(16)))
        val encrypted = cipher.doFinal(input.toByteArray())
        return Base64.getEncoder().encodeToString(encrypted)
    }

    private fun decrypt(input: String): String {
        val cipher = Cipher.getInstance("AES/CBC/PKCS5Padding")
        val key = SecretKeySpec(key.toByteArray(charset("UTF-8")), "AES")
        cipher.init(Cipher.DECRYPT_MODE, key, IvParameterSpec(ByteArray(16)))
        val decrypted = cipher.doFinal(Base64.getDecoder().decode(input))
        return String(decrypted, Charset.forName("UTF-8"))
    }
}