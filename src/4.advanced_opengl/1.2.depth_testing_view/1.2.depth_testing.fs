#version 330 core
out vec4 FragColor;

float near = 0.1; 
float far = 100.0; 
float LinearizeDepth(float depth) // wyh 入参应该是[0, 1]范围的非线性的z, 非线性的z是投影矩阵的过程中得到的; 疑问: 所以z在裁切空间除以w后, 不是在[-1, 1]吗?
{
    float z = depth * 2.0 - 1.0; // back to NDC // wyh 先把非线性z转换成[-1, 1]范围内, 所谓的NDC坐标空间
    return (2.0 * near * far) / (far + near - z * (far - near)); // wyh 然后转换回线性标准的z, 投影矩阵变换前的z, 范围应该是[near, far], 远大于[0, 1]
    // wyh 这个计算到时候去看看Unity Shader和GAMES101TOUYI的投影矩阵推算相关
    // wyh 疑问: 投影矩阵前z [near, far] (刚刚裁剪), 投影矩阵后z [0, 1], 难道不是[-1, 1](3D NDC)或者[-w, w](4D 裁剪空间)吗?
}

void main()
{             
    float depth = LinearizeDepth(gl_FragCoord.z) / far; // divide by far to get depth in range [0,1] for visualization purposes
    FragColor = vec4(vec3(depth), 1.0); // wyh 由于把深度转换成了灰度颜色(RGB3项都为z), 且范围[near, far], 大于1的部分就是全白了, 为了方便观察, 这里除以了far
}
// wyh gl_FragCoord第一次出现, 也是glsl的内建变量, 其xy是屏幕空间坐标, z是片段的深度值