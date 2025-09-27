shader_type spatial;

varying vec3 vworld_position;

uniform sampler2D albedo_texture;
uniform float scale = 0.1;
uniform float blend_sharpness = 10.0;

void vertex()
{
	vworld_position = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
}

void fragment() {
	// Called for every pixel the material is visible on.
	
    vec3 wnorm = abs(NORMAL);
	
	vec3 blend_weights = normalize(wnorm);
	blend_weights = pow(blend_weights, vec3(blend_sharpness));
	blend_weights /= (blend_weights.x + blend_weights.y + blend_weights.z);
	
	vec2 uv_x = vworld_position.yz * scale;
	vec3 sample_x = texture(albedo_texture, uv_x).rgb;
	
	vec2 uv_y = vworld_position.xz * scale;
	vec3 sample_y = texture(albedo_texture, uv_y).rgb;
	
	vec2 uv_z = vworld_position.xy * scale;
	vec3 sample_z = texture(albedo_texture, uv_z).rgb;
	
	vec3 blended_color = sample_x * blend_weights.x + sample_y * blend_weights.y + sample_z * blend_weights.z;
    
    ALBEDO = blended_color;
}
