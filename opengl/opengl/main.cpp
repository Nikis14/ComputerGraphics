#include "GL/soil.h"
#include "GL/glew.h"
#include "GL/freeglut.h"
#include <assimp/Importer.hpp>
#include <assimp/Exporter.hpp>
#include <assimp/scene.h>
#include <assimp/postprocess.h>
#include <iostream>
#include "glm.hpp"
#include <vector>
#include <random>
using namespace std;

int byterandom()
{
	return (rand() % 100+155);
}

struct Vertex
{
	glm::vec3 position;
	glm::vec3 normal;
};

struct Mesh
{
public:
	//The vertex array object, vertex buffer object and element buffer object
	unsigned int VAO;
	GLuint VBO;
	GLuint EBO;
	//Vectors for the vertices and indices to put in the buffers
	std::vector<Vertex> vertices;
	std::vector<GLuint> indices;

	//Constructor
	Mesh(const std::vector<Vertex>& vertices, const std::vector<GLuint>& indices)
	{

		this->vertices = vertices;
		this->indices = indices;
		//Generate the VAO
		glGenVertexArrays(1, &VAO);

		//Generate the buffer objects
		glGenBuffers(1, &VBO);
		glGenBuffers(1, &EBO);

		//Bind the VAO
		glBindVertexArray(VAO);

		//Bind the VBO and set the vertices
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * vertices.size(), &vertices.at(0), GL_STATIC_DRAW);

		//Enable the first attribute pointer
		glEnableVertexAttribArray(0);
		//Set the attribute pointer    The stride is meant to be 'sizeof(Vertex)', but it doesn't work at all that way
		//                                              \/
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), 0);

		//Enable the second attribute pointer
		glEnableVertexAttribArray(1);
		//Set the attribute pointer                   ditto
		//                                              \/
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, normal));

	

		//Bind the EBO and set the indices
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(GLuint) * indices.size(), &indices.at(0), GL_STATIC_DRAW);

		//Report any errors
		GLenum error = glGetError();
		if (error != GL_NO_ERROR)
		{
			std::cerr << "Error while creating mesh!" << std::endl;
		}

		glBindVertexArray(0);
	}

	void draw()
	{
		//Bind the VAO
		glBindVertexArray(VAO);
		//Bind the ELement Buffer Object
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
		GLuint vbocolors = GLuint();
		glGenBuffers(1, &vbocolors);
		std::random_device engine;
		unsigned x = engine();
		//glBindBuffer(GL_ARRAY_BUFFER, vbocolors);
		//glBufferData(GL_ARRAY_BUFFER, sizeof(colors), &colors, GL_STATIC_DRAW);
		//glColorPointer(sizeof(indices)*3, GL_BYTE, 0, colors);
		for (int start = 0; start < size(vertices); start += 3) {
			// Set the color of the triangle
			char c1 = engine();
			char c2 = engine();
			char c3 = engine();
			glColor3b(c1,c2,c3);
			// Draw a single triangle
			glDrawArrays(GL_TRIANGLES, start, 3);
		}
		//Unbind the VAO
		glBindVertexArray(0);
	}
};





static int w = 0, h = 0;

GLuint floor_texture_id;
GLuint car_texture_id;
GLuint knuckles_texture_id;
GLuint uganda_texture_id;

GLfloat dist_x = 0, dist_y = 0;
GLfloat angle = 0;

GLfloat machine_coord_x = 0, machine_coord_y = 0;
GLfloat machine_angle = 0;

GLfloat cam_dist = 20;
GLfloat ang_hor = 0, ang_vert = -60;

GLfloat no_light[] = { 0, 0, 0, 1 };
GLfloat light[] = { 1, 1, 1, 0 };

double cam_x = 0;
double cam_y = 0;
double cam_z = 0;

float amb[] = { 0.8, 0.8, 0.8 };
float dif[] = { 0.2, 0.2, 0.2 };

const double step = 1;



const aiScene* load_from_file()
{
	Assimp::Importer importer;
	const aiScene* scene = importer.ReadFile("Low-Poly-Racing-Car.obj", aiProcess_Triangulate | aiProcess_GenNormals);

	//Check for errors
	/*if ((!scene) || (scene->mFlags == AI_SCENE_FLAGS_INCOMPLETE) || (!scene->mRootNode))
	{
		std::cerr << "Error loading mymodel.obj: " << std::string(importer.GetErrorString()) << std::endl;
		//Return fail
		return;
	}*/

	//A vector to store the meshes
	std::vector<std::unique_ptr<Mesh> > meshes;
	//Iterate over the meshes
	for (unsigned int i = 0; i < scene->mNumMeshes; ++i)
	{
		//Get the mesh
		aiMesh* mesh = scene->mMeshes[i];

		//Create vectors for the vertices and indices
		std::vector<Vertex> vertices;
		std::vector<GLuint> indices;

		//Iterate over the vertices of the mesh
		for (unsigned int j = 0; j < mesh->mNumVertices; ++j)
		{
			//Create a vertex to store the mesh's vertices temporarily
			Vertex tempVertex;

			//Set the positions
			tempVertex.position.x = mesh->mVertices[j].x;
			tempVertex.position.y = mesh->mVertices[j].y;
			tempVertex.position.z = mesh->mVertices[j].z;
			
			//Set the normals
			tempVertex.normal.x = mesh->mNormals[j].x;
			tempVertex.normal.y = mesh->mNormals[j].y;
			tempVertex.normal.z = mesh->mNormals[j].z;
             		
			//Add the vertex to the vertices vector
			vertices.push_back(tempVertex);
		}

		//Iterate over the faces of the mesh
		for (unsigned int j = 0; j < mesh->mNumFaces; ++j)
		{
			//Get the face
			aiFace face = mesh->mFaces[j];
			//Add the indices of the face to the vector
			for (unsigned int k = 0; k < face.mNumIndices; ++k) { indices.push_back(face.mIndices[k]); }
		}

		//Create a new mesh and add it to the vector
		meshes.push_back(std::unique_ptr<Mesh>(new Mesh(std::move(vertices), std::move(indices))));

	}
	for (auto& mesh : meshes) { mesh.get()->draw(); }
	return scene;
}

void save_to_file()
{
	const aiScene* loaded = load_from_file();
	Assimp::Exporter exporter;
	const aiExportFormatDesc* format = exporter.GetExportFormatDescription(1);
	//const string path = Filename.substr(0,lIndex+1);
	string path = "..\\ttt.obj";
	cout << "\tExport path: " << path << endl;
	aiReturn ret = exporter.Export(loaded, format->id, path);
	cout << "OK!";
	cout << exporter.GetErrorString() << endl;

}


void init() {
	glClearColor(0.9, 0.9, 0.9, 1);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_COLOR_MATERIAL);
	save_to_file();
}

void update() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();
	

	double ang_vert_r = ang_vert / 180 * 3.1416;
	double ang_hor_r = ang_hor / 180 * 3.1416;
	cam_x = cam_dist * std::sin(ang_vert_r) * std::cos(ang_hor_r);
	cam_y = cam_dist * std::sin(ang_vert_r) * std::sin(ang_hor_r);
	cam_z = cam_dist * std::cos(ang_vert_r);

	gluLookAt(cam_x, cam_y, cam_z, 0., 0., 0., 0., 0., 1.);
	glTranslatef(5.0f, 0.0f, -10.0f);
	load_from_file();
    glFlush();
    glutSwapBuffers();
}

void updateCamera() {
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluPerspective(60.f, (float)w / h, 1.0f, 1000.f);
    glMatrixMode(GL_MODELVIEW);
}


void keyboard(unsigned char key, int x, int y) {

}

void reshape(int width, int height) {
    w = width;
    h = height;

    glViewport(0, 0, w, h);
    updateCamera();
}

void SpecialKeys(int key, int x, int y) {
}

int main(int argc, char* argv[]) {
    glutInit(&argc, argv);
    glutInitWindowPosition(100, 100);
    glutInitWindowSize(800, 800);
    glutCreateWindow("texture and lighting");
	glewInit();
    init();

    glutReshapeFunc(reshape);
    glutDisplayFunc(update);
    glutKeyboardFunc(keyboard);
    glutSpecialFunc(SpecialKeys);

    glutMainLoop();

    return 0;
}
