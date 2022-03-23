#version 330 core
layout (triangles) in;
layout (triangle_strip, max_vertices=18) out; // wyh 这好像是集合着色

uniform mat4 shadowMatrices[6]; // wyh 这个矩阵用来干嘛?

out vec4 FragPos; // FragPos from GS (output per emitvertex)

// wyh 几何着色器有一个内建变量叫做gl_Layer，它指定发散出基本图形送到立方体贴图的哪个面。当不管它时，几何着色器就会像往常一样把它的基本图形发送到输送管道的下一阶段，
// wyh 但当我们更新这个变量就能控制每个基本图形将渲染到立方体贴图的哪一个面。当然这只有当我们有了一个附加到激活的帧缓冲的立方体贴图纹理才有效; "gl_Layer"

void main()
{
    for(int face = 0; face < 6; ++face) // wyh 立方体深度贴图的6个面
    {
        gl_Layer = face; // built-in variable that specifies to which face we render.
        for(int i = 0; i < 3; ++i) // for each triangle's vertices
        {
            FragPos = gl_in[i].gl_Position;
            gl_Position = shadowMatrices[face] * FragPos; // wyh 1分为6, 继续传递坐标, 这里×的shadowMatrices应该是gs对1分为6的位置变换
            EmitVertex();
        }    
        EndPrimitive(); // wyh gs的常规发射两件套
    }
} 