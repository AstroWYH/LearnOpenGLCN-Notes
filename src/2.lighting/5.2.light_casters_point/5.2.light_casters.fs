#version 330 core
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;    
    float shininess;
}; 

struct Light {
    vec3 position; // wyh 点光源 还原了灯的pos位置向量(w = 1)
  
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
	
    float constant; // wyh 增加点光源衰减的常数项、一次项、二次项
    float linear;
    float quadratic;
};

in vec3 FragPos;  
in vec3 Normal;  
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    // ambient
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos); // wyh 还原点光源的光照方向计算公式
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;  
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;  
    
    // attenuation
    float distance    = length(light.position - FragPos); // wyh 距离d用于计算光衰
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance)); // wyh 点光源光衰

    ambient  *= attenuation; // wyh 光衰叠加
    diffuse   *= attenuation;
    specular *= attenuation;   
        
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 