from OpenGL.GL import *
from OpenGL.GLUT import *
from OpenGL.GLU import *
import msvcrt

w,h= 500,500

rotation_dir_whole = 1
rotate_x_whole=0
rotate_y_whole=0
rotate_z_whole=0
rotate_angle_whole = 0
rotate_x_silver=-0.1
rotate_y_silver=0
rotate_z_silver=0
rotate_angle_silver=0
rotate_x_gold=0
rotate_y_gold=0
rotate_z_gold=0
rotate_angle_gold=0
rotate_x_bronze=0.1
rotate_y_bronze=0
rotate_z_bronze=0
rotate_angle_bronze=0

def iterate():
    glViewport(0, 0, 500, 500)
    glMatrixMode(GL_MODELVIEW)
    glLoadIdentity()
    glOrtho(0.0, 500, 0.0, 500, 0.0, 1.0)
    glMatrixMode (GL_PROJECTION)
    glLoadIdentity()

def draw_silver_stand():
    glPushMatrix()
    glTranslate(0, 0, 0)
    glRotate(rotate_angle_silver,rotate_x_silver,rotate_y_silver,rotate_z_silver)
    glScale(1, 2, 1)
    glColor3f(0.9, 0.91, 0.98)
    glutSolidCube(0.1)
    glPopMatrix()

def draw_gold_stand():
    glPushMatrix()
    glTranslate(0.1, 0.05, 0)
    glRotate(rotate_angle_gold, rotate_x_gold, rotate_y_gold, rotate_z_gold)
    glScale(1, 3, 1)
    glColor3f(0.81, 0.71, 0.23)
    glutSolidCube(0.1)
    glPopMatrix()

def draw_bronze_stand():
    glPushMatrix()
    glTranslate(-0.1, -0.05, 0)
    glRotate(rotate_angle_gold, rotate_x_gold, rotate_y_gold, rotate_z_gold)
    glColor3d(0.55, 0.47, 0.14)
    glutSolidCube(0.1)
    glPopMatrix()

def draw_red_stand():
    glPushMatrix()
    glTranslate(0, -0.1, 0)
    glRotate(rotate_angle_gold, rotate_x_gold, rotate_y_gold, rotate_z_gold)
    glColor3d(1, 0.0, 0.0)
    glutSolidCube(0.1)
    glPopMatrix()

def draw_all():
    glPushMatrix()
    glRotate(rotate_angle_whole, 1, 0, 0)
    draw_bronze_stand()
    draw_silver_stand()
    draw_gold_stand()
    #draw_red_stand()
    glPopMatrix()

def rotation_serv_inverter(angle):
    global rotate_angle_whole
    if rotate_angle_whole + angle == 360:
        return -360
    elif rotate_angle_whole + angle == -360:
        return 360
    return rotate_angle_whole + angle

def process_keys(key,x,y):
    global rotate_angle_whole
    if key == GLUT_KEY_RIGHT:
        rotate_angle_whole -=10 #rotation_serv_inverter(10)
    elif key == GLUT_KEY_LEFT:
        rotate_angle_whole +=10 #rotation_serv_inverter(-10)
    print(rotate_angle_whole)


def showScreen():
    #key = msvcrt.getch()
    #print(key)
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
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
glutDisplayFunc(showScreen)
glutSpecialFunc(process_keys)
glutIdleFunc(showScreen)
glutMainLoop()