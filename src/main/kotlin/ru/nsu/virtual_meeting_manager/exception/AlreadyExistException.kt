package ru.nsu.virtual_meeting_manager.exception

data class AlreadyExistException(
    override val message: String? = null,
) : Exception(message)