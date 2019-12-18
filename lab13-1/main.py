from OpenGL.GL import *
from OpenGL.GLUT import *
from OpenGL.GLU import *
from assimploader import load_file

def showScreen():
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
    glLoadIdentity()
    glPushMatrix()
    glTranslate(0,10,0)
    scene = load_file("Car.obj")
    ctr = 0
    while ctr <  scene.meshes[0].vertices.shape[0]:
        pointdata = scene.meshes[0].vertices[ctr:ctr+3]
        ctr +=3
        glVertexPointer(3, GL_FLOAT, 0, pointdata)
        pointcolor = [[1, 0, 0], [0, 1, 0], [0, 0, 1]]
        glColorPointer(3, GL_FLOAT, 0, pointcolor)
        glDrawArrays(GL_TRIANGLES, 20, 3)
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
glutIdleFunc(showScreen)
glutMainLoop()