from OpenGL.GL import *
from OpenGL.GLUT import *
from OpenGL.GLU import *
import glm
import numpy
import math
pointdata = [[0, 0.5, 0], [-0.5, -0.5, 0], [0.5, -0.5, 0]]
pointcolor = [[1, 0, 0], [0, 1, 0], [0, 0, 1]]
x_scale = y_scale = 1
y_rotate = 0

def gen_mat_3(y_rotate,x_scale,y_scale):
    model = glm.mat4(1)
    model = glm.rotate(model, y_rotate, glm.vec3(0, 1, 0))
    model = glm.scale(model, glm.vec3(x_scale, y_scale, 1))
    return model

def load_shader_from_file(filename):
    f = open(filename,"r")
    return f.read()

def create_shader(shader_type, filename):
    global location_y
    shader = glCreateShader(shader_type)
    glShaderSource(shader, load_shader_from_file(filename))
    glCompileShader(shader)
    return shader

def draw_all():
    global y_rotate
    glClear(GL_COLOR_BUFFER_BIT)  # Очищаем экран и заливаем серым цветом
    glEnableClientState(GL_VERTEX_ARRAY)  # Включаем использование массива вершин
    glEnableClientState(GL_COLOR_ARRAY)  # Включаем использование массива цветов
    glVertexPointer(3, GL_FLOAT, 0, pointdata)
    glColorPointer(3, GL_FLOAT, 0, pointcolor)
    matr = gen_mat_3(y_rotate,x_scale,y_scale)
    glProgramUniformMatrix4fv(program,matr_rot_loc,1,GL_FALSE,glm.value_ptr(matr))
    glDrawArrays(GL_TRIANGLES, 0, 3)
    glDisableClientState(GL_VERTEX_ARRAY)  # Отключаем использование массива вершин
    glDisableClientState(GL_COLOR_ARRAY)  # Отключаем использование массива цветов
    glutSwapBuffers()


def specialkeys(key, x, y):
    # Сообщаем о необходимости использовать глобального массива pointcolor
    global y_scale,x_scale,y_rotate
    # Обработчики специальных клавиш
    if key == GLUT_KEY_UP:
        y_scale += 0.1
    if key == GLUT_KEY_DOWN:
        y_scale -= 0.1
    if key == GLUT_KEY_LEFT:
        x_scale -= 0.1
    if key == GLUT_KEY_RIGHT:
        x_scale += 0.1
    if key == GLUT_KEY_F1:
        y_rotate += 10
    if key == GLUT_KEY_F2:
        y_rotate -= 10



def showScreen():
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
    #glOrtho(0.0, 500, 0.0, 500, 0.0, 1.0)
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    draw_all()
    glFlush()
    glutSwapBuffers()

    #glutPostRedisplay()


glutInit()
glutInitDisplayMode(GLUT_RGBA)
glutInitWindowSize(1000, 1000)
glutInitWindowPosition(0, 0)
wind = glutCreateWindow("OpenGL Coding Practice")
glEnable(GL_DEPTH_TEST)
glutDisplayFunc(showScreen)
glutSpecialFunc(specialkeys)
glutIdleFunc(showScreen)
program = glCreateProgram()
glAttachShader(program, create_shader(GL_FRAGMENT_SHADER, "fragment_shader.shd"))
glAttachShader(program, create_shader(GL_VERTEX_SHADER, "vertex_shader.shd"))
glLinkProgram(program)
glUseProgram(program)
matr_rot_loc = glGetUniformLocation(program,"rot")
glutMainLoop()