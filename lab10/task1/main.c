#include <GLUT/glut.h>
#include "figures.h"

typedef void(*Function) (void);

int n = 0;
const int primitivesCount = 9;
const Function primitives[primitivesCount] =
{
    teapot,
    cube,
    sphere,
    torus,
    icosahedron,
    octehedron,
    
    rect,
    triangle2,
    triangle
};


float rotate_x = 0;
float rotate_y = 0;
float rotate_z = 0;

float r = 0, g = 0, b = 0;

void setRandomColor()
{
    r = (rand() % 255) / 255.0;
    g = (rand() % 255) / 255.0;
    b = (rand() % 255) / 255.0;
}

void render()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glClearColor(0.23, 0.23, 0.23, 1.0);

    glRotatef(rotate_x, 1, 0, 0);
    glRotatef(rotate_y, 0, 1, 0);
    glRotatef(rotate_z, 0, 0, 1);

    glColor3f(r, g, b);
    primitives[n]();

    glLoadIdentity();
    glutSwapBuffers();
}

void mouseHandler(int button, int state, int x, int y)
{
    if (button == GLUT_LEFT_BUTTON && state == GLUT_DOWN)
    {
        setRandomColor();
        n = rand() % primitivesCount;
    }
}

void keyHandler(int key, int x, int y)
{
    switch (key)
    {
    case GLUT_KEY_UP: rotate_x += 5; break;
    case GLUT_KEY_DOWN: rotate_x -= 5; break;
    case GLUT_KEY_LEFT: rotate_y += 5; break;
    case GLUT_KEY_RIGHT: rotate_y -= 5; break;
    case GLUT_KEY_PAGE_UP: rotate_z += 5; break;
    case GLUT_KEY_PAGE_DOWN: rotate_z -= 5; break;
    default:
        break;
    }
    glutPostRedisplay();
}


int main(int argc, char** argv){
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
    glutInitWindowPosition(20, 20);
    glutInitWindowSize(800, 600);
    glutCreateWindow("OpenGL Window");

    glEnable(GL_DEPTH_TEST);

    glutDisplayFunc(render);
    glutMouseFunc(mouseHandler);
    glutSpecialFunc(keyHandler);
    glutMainLoop();

    return 0;
}
