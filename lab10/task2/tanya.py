from OpenGL.GL import *
from OpenGL.GLUT import *
from OpenGL.GLU import *
import random

r = g= b =0

def teapot():
    glutWireTeapot(0.55)

def process_keys(key,x,y):
    global rotate_angle_whole,rotate_angle_silver,rotate_angle_bronze,rotate_angle_gold,rotate_scene
    pass

def set_random_color():
    global r,g,b
    r = random.choice(range(256))
    g = random.choice(range(256))
    b = random.choice(range(256))

def process_mouse(key,state,x,y):
    if (key == GLUT_LEFT_BUTTON) and state == GLUT_DOWN:
        set_random_color()

def render():
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
    glColor3f(r,g,b)
    teapot()
    glLoadIdentity()
    glFlush()
    glutSwapBuffers()

def main():
    glutInit()
    glutInitDisplayMode(GLUT_DEPTH|GLUT_DOWN|GLUT_RGBA)
    glutInitWindowSize(600,400)
    glutCreateWindow("Tanya")
    glEnable(GL_DEPTH_TEST)
    glutDisplayFunc(render)
    glutMouseFunc(process_mouse)
    glutMainLoop()

main()