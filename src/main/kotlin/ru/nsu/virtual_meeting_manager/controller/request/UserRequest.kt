package ru.nsu.virtual_meeting_manager.controller.request

data class UserRequest (
    val email: String,
    val password: String,
    val name: String,
    val surname: String,
    val gender: String,
    val birth: String,
)