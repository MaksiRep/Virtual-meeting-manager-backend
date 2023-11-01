package ru.nsu.virtual_meeting_manager.controller

import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.*
import ru.nsu.virtual_meeting_manager.controller.request.UserRequest
import ru.nsu.virtual_meeting_manager.service.UserService

@RestController
class UserController (val userService: UserService) {

    @PostMapping("/register")
    @ResponseStatus(HttpStatus.CREATED)
    fun registerUser (@RequestBody userRequest: UserRequest) {
        userService.register(userRequest)
    }
}