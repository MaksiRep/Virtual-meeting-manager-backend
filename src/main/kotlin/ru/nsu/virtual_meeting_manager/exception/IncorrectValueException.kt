package ru.nsu.virtual_meeting_manager.exception

data class IncorrectValueException(
    override val message: String? = null,
) : Exception(message)