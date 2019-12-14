from OpenGL.GL import *
from OpenGL.GLUT import *
from OpenGL.GLU import *
pointdata = [[0, 0.5, 0], [-0.5, -0.5, 0], [0.5, -0.5, 0]]
pointcolor = [[1, 0, 0], [0, 1, 0], [0, 0, 1]]
x_scale = y_scale = 1

def draw_all():
    glEnableClientState(GL_VERTEX_ARRAY)  # Включаем использование массива вершин
    glEnableClientState(GL_COLOR_ARRAY)  # Включаем использование массива цветов
    glVertexPointer(3, GL_FLOAT, 0, pointdata)
    glColorPointer(3, GL_FLOAT, 0, pointcolor)
    glDrawArrays(GL_TRIANGLES, 0, 3)
    glDisableClientState(GL_VERTEX_ARRAY)  # Отключаем использование массива вершин
    glDisableClientState(GL_COLOR_ARRAY)  # Отключаем использование массива цветов
    glutSwapBuffers()

def rotation_serv(global_angle,angle):
    return (global_angle+ angle)%360

def specialkeys(key, x, y):
    # Сообщаем о необходимости использовать глобального массива pointcolor
    global y_scale,x_scale
    # Обработчики специальных клавиш
    if key == GLUT_KEY_UP:
        y_scale +=0.1
    if key == GLUT_KEY_DOWN:
        y_scale -=0.1
    if key == GLUT_KEY_LEFT:
        x_scale +=0.1
    if key == GLUT_KEY_RIGHT:
        x_scale -=0.1


def showScreen():
    #key = msvcrt.getch()
    #print(key)
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
    glLoadIdentity()
    glPushMatrix()
    draw_all()
    glPopMatrix()
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
glutMainLoop()