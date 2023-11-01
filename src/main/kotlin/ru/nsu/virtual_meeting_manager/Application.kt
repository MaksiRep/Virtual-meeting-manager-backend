package ru.nsu.virtual_meeting_manager

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.boot.web.servlet.ServletComponentScan

@ServletComponentScan
@SpringBootApplication
class Application

fun main(args : Array<String>) {
    runApplication<Application>(*args)
}